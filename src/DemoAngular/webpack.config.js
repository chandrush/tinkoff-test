var webpack = require('webpack');
require('babel-polyfill');
var autoprefixer = require('autoprefixer');

var devFlagPlugin = new webpack.DefinePlugin({
	__DEV__: JSON.stringify(JSON.parse(process.env.DEBUG || 'false'))
});

function configure(release)
{
	var DEBUG = !release;

	var BABEL_LOADER = "babel-loader";
	if (DEBUG)
		BABEL_LOADER += "?presets[]=es2016,presets[]=stage-0";//
	else
		BABEL_LOADER += "?presets[]=es2016,presets[]=stage-0,plugins[]=transform-runtime,plugins[]=transform-remove-debugger,plugins[]=transform-remove-console";

	var config = {
		entry: {
			app: "./App/js/app.js"
		},
		output: {
			path: "./wwwroot/",
			filename: "[name].js"
		},
		module: {
			loaders: [
				{
					test: /\.jsx?$/,
					exclude: /(node_modules|bower_components)/,
					loader: BABEL_LOADER,
				},
				{
					test: /\.tsx?$/,
					exclude: /(node_modules|bower_components)/,
					loader: BABEL_LOADER + "!ts-loader"
				},
				{
					test: /\.css$/,
					loader: 'style-loader!css-loader!postcss-loader'
				},
				{
					test: /\.less$/,
					loader: 'style-loader!css-loader!postcss-loader!less-loader'
				},
				{
					test: /\.scss$/,
					loader: "style-loader!css-loader!postcss-loader!sass-loader"
				},
				{
					test: /\.(png|jpg)$/,
					exclude: /(node_modules|bower_components)/,
					loader: 'file-loader?name=/[sha512:hash:base64:7].[ext]'
				},
				{
					test: /\.html$/,
					exclude: /(node_modules|bower_components)/,
					loader: "html",
				},
			]
		},
		postcss: [autoprefixer({ browsers: ['> 2%', 'last 2 versions'] })],
		resolve: {
			extensions: ['', '.webpack.js', '.web.js', '.js', '.jsx', '.ts', '.tsx', ".html"]
		},
		plugins: [
			new webpack.optimize.OccurenceOrderPlugin()
		].concat(DEBUG ? [] : [
		  new webpack.optimize.DedupePlugin(),
		  new webpack.optimize.UglifyJsPlugin(),
		  new webpack.optimize.AggressiveMergingPlugin(),
		  devFlagPlugin
		]),
		devtool: (DEBUG ? 'source-map' : 'source-map')
	};

	//var testConfig = Object.assign({}, config);
	//testConfig.entry = Object.assign({ test: './webpack.tests.js' }, config.entry);
	//return [config, testConfig];

	return [config, config];
}

module.exports = configure;