module.exports = function (ngModule)
{
	require("./homeCtrl/homeCtrl.js")(ngModule);
	require("./historyCtrl/historyCtrl.js")(ngModule);
}
