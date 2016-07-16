module.exports = function (ngModule)
{
	ngModule.controller("HistoryCtrl", ['$scope', '$http', function ($scope, $http)
	{
		$scope.links = [];

		$http.get("/api/bitly")
			.success(function (data, status, headers, config)
			{
				$scope.links = data;
			})
			.error(function () { });
	}]);
}
