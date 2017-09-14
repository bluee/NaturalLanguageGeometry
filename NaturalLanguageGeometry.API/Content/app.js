var app = angular.module('app', []);

app.controller('natLangGeometryCtrl', function ($scope, commonRepository) {

    $scope.naturalText = "";
    $scope.errorMessage = "";

    $scope.suggestions = [
        "Draw a circle with a radius of 100",
        "Draw a rectangle with a width of 250 and a height of 400",
        "Draw an octagon with a side of 200",
        //"Draw an isosceles triangle with a height of 200 and a width of 100",
        "Draw a triangle with a side of 100",
        "Draw a heptagon with a side of 100",
        "Draw a square with a side of 100"
    ];

    $scope.parse = function () {
        $scope.errorMessage = "";

        paper.install(window);
        var canvas = document.getElementById("canvas");
        paper.setup(canvas);

        commonRepository.get({ Text: $scope.naturalText }).then(
            function (response) {
                
                //Ovals -https://en.wikipedia.org/wiki/Oval, http://paperjs.org/reference/shape/#shape-ellipse-rectangle
                //Parallelograms -https://en.wikipedia.org/wiki/Parallelogram, https://github.com/paperjs/paper.js/issues/283
                //Isosceles triangle: An isosceles triangle is a triangle with(at least) two equal sides.
                
                if (!response) {
                    $scope.errorMessage = 'Sorry could not understand your request';
                    return;
                }
                var xy = 10 + response.SizeX;
                switch (response.Shape) {
                    case "rectangle": //4
                        var path = new Path.Rectangle([10, 10], [response.SizeX, response.SizeY]);
                        break;

                    case "heptagon":
                        var path = new Path.RegularPolygon(new Point(xy, xy), 7, response.SizeX);
                        break;

                    case "square":
                        var path = new Path.Rectangle([10, 10], [response.SizeX, response.SizeY]);
                        break;

                    case "triangle": //3
                        var path = new Path.RegularPolygon(new Point(xy, xy), 3, response.SizeX);
                        break;

                    case "pentagon":
                        var path = new Path.RegularPolygon(new Point(1, 1), 5, response.SizeX);
                        break;

                    case "hexagon": 
                        var path = new Path.RegularPolygon(new Point(1, 1), 6, response.SizeX);
                        break;

                        //7

                    case "octagon":
                        var path = new Path.RegularPolygon(new Point(xy, xy), 8, response.SizeX);
                        break;

                    case "decagon":
                        var path = new Path.RegularPolygon(new Point(80, 70), 10, response.SizeX);
                        break;


                    case "circle":
                        var path = new Path.Circle(new Point(xy, xy), response.SizeX);
                        break;
                    
                    default: //oval, parallelogram, isosceles
                        $scope.errorMessage = "Sorry, shape not supported";
                }
                path.fillColor = '#e9e9ff';
                path.selected = true;
                paper.view.draw();




            },
            function (error) {
                alert('An error occurred');
                //handle error
            }
        );

    };

});

app.factory('commonRepository', function ($http, $q) {
    return {
        get: function (queryRequest) {
            var deferred = $q.defer();
            $http.post("/api/parse", queryRequest).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
    }
});