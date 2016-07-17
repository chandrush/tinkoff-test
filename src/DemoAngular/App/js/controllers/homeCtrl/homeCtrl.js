module.exports = function (ngModule)
{
	ngModule.controller("HomeCtrl", ['$scope', '$http', function ($scope, $http)
	{
		$scope.link;

		$scope.inputDisabled = false;

		$scope.inputValidationErrorMessage = '';

		$scope.shorten = function()
		{
			if ($scope.link)
			{
				$scope.inputDisabled = true;
				$http.post("/api/bitly", $scope.link)
				.success(function (data, status, headers, config)
				{
					$scope.link = data;
				})
				.error(function () { })
				.then(function ()
				{
					$scope.inputDisabled = false;
				});
			}
		}

		$scope.isInutValid = function()
		{
			return scope.inputValidationErrorMessage && $scope.inputValidationErrorMessage.length > 0;
		}

	}]);
}