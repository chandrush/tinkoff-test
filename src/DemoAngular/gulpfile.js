var gulp = require('gulp');
var del = require('del');
var runSequence = require("run-sequence");
var sass = require("gulp-sass");
var webpack = require("webpack");
var gutil = require('gulp-util');
var postcss = require("gulp-postcss");
var autoprefixer = require("autoprefixer");

var paths = {
	node_modules: "./node_modules/",
	webroot: "./wwwroot/",
	app: "./App/",
	appStyles: "./App/styles/",
	bootstrapDir: "./node_modules/bootstrap/dist/",
	angularDir: "./node_modules/angular/"
};

gulp.task("clean", function ()
{
	del([paths.webroot + "/*", "!" + paths.webroot + "/bin"]);
});

/**
* Build&copy sass styles from the Styles folder - the folder for the global styles.
*/
gulp.task("styles", function ()
{
	return gulp.src(paths.appStyles + "site.scss")
		.pipe(sass().on("error", sass.logError))
		.pipe(postcss([autoprefixer({ browsers: ["> 2%", "last 2 versions"] })]))
		.pipe(gulp.dest(paths.webroot));
});

gulp.task('styles:watch', function ()
{
	gulp.watch(paths.appStyles + "site.scss", ["styles"]);
});

/**
* Copy static html to webroot folder.
*/
gulp.task("webconfig", function ()
{
	return gulp.src("web.config")
	  .pipe(gulp.dest(paths.webroot + "/"));
});

/**
* Copy static assets to webroot folder.
*/
gulp.task("assets", function ()
{
	var assets = [
		paths.bootstrapDir + "css/bootstrap.min.css",
		paths.angularDir + "angular.min.js"
	];
	return gulp.src(assets)
	  .pipe(gulp.dest(paths.webroot + "/"));
});

/**
* Webpack bundling for debugging.
*/
gulp.task("bundle:debug", function ()
{
	var config = require("./webpack.config.js");
	console.log(JSON.stringify(config(false)[1]));
	webpack(config(false)[1]).watch(100, webpackCompilationCallback);
});

/**
* Webpack bundling for release.
*/
gulp.task("bundle:release", function ()
{
	var config = require("./webpack.config.js");
	console.log(JSON.stringify(config(true)[0]));
	webpack(config(true)[0]).run(webpackCompilationCallback);
});

function webpackCompilationCallback(err, stats)
{
	if (err)
		throw new gutil.PluginError("webpack", err);

	var jsonStats = stats.toJson();

	if (jsonStats.errors.length > 0)
		gutil.log(jsonStats.errors.toString({ colors: true }));
	else if (jsonStats.warnings.length > 0)
		gutil.log(jsonStats.warnings.toString({ colors: true }));

	gutil.log("webpack rebuilt at " + (new Date()).toLocaleTimeString());
}

/**
* Debug build.
*/
gulp.task("build:debug", function (cb)
{
	runSequence(["clean", "styles", "webconfig", "bundle:debug", "assets", "styles:watch"], cb);
});

/**
* Release build.
*/
gulp.task("build:release", function (cb)
{
	runSequence(["clean", "styles", "webconfig", "bundle:release", "assets"], cb);
});
