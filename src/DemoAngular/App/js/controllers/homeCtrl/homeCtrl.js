module.exports = function (ngModule)
{
	ngModule.controller("HomeCtrl", ['$scope', '$http', function ($scope, $http)
	{
		$scope.link;

		$scope.inputDisabled = false;

		$scope.inputValidationErrorMessage = '';

		$scope.submitForm = function ()
		{
			if ($scope.link)
			{
				$scope.inputDisabled = true;
				$http({
					method: 'POST',
					url: "/api/bitly",
					data: JSON.stringify($scope.link),
					headers: { 'Content-Type': 'application/json' }
				})
				.then(function (response)
				{
					$scope.link = response.data;
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