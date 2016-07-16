module.exports = function (ngModule)
{
	ngModule.config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider)
	{
		$locationProvider.html5Mode({
			enabled: true,
			requireBase: false
		});

		$routeProvider
			.when("/",
			{
				template: require("./templates/home.html"),
				controller: "HomeCtrl"
			})
			.when("/history",
			{
				template: require("./templates/history.html"),
				controller: "HistoryCtrl"
			})
			.otherwise({ redirectTo: "/" });
	}]);
};