const angular = require("angular");
const angularRoute = require("angular-route");

const bitlyApp = angular.module('bitlyApp', [angularRoute]);

require("./directives")(bitlyApp);
require("./controllers")(bitlyApp);
require("./app.config.js")(bitlyApp);



