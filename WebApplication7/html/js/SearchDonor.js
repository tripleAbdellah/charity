var myApp = angular.module('myApp', ['angularUtils.directives.dirPagination', 'ui.bootstrap']);


function ModalInstanceCtrl($scope, $modalInstance, myService ,donor) {

    $scope.donor = donor;
    
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};


function userController($scope, $http, myService, $modal, $log) {


    $scope.open = function (donor) {

        var modalInstance = $modal.open({
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            size: 'lg',
            resolve: {
                donor: function () {
                    return donor;
                }
            }
        });
    };


    $scope.searchDonors = function () {
        var url = $scope.getUrl();
        $scope.loading = true;
        $http(
            {
                url: url,
                method: "GET",
                headers: { 'Content-Type': 'application/json' }
            }
            ).success(function (data, status, headers, config) {
                $scope.donors = data;
                $scope.loading = false;
            }
            ).error(function (data, status, headers, config) {
                $scope.status = 'Error occurred: ' + status + ' ' + headers;
                $scope.loading = false;
            }
        ); 
    }

    $scope.getUrl = function () {

        // return '/api/donor/getDonors/?Name=' + $scope.FullName + '&Code=' + $scope.donorCode;

        return '/api/donor/getDonors/?' + $scope.getQueryParameter();

    };

    $scope.getQueryParameter = function () {
        var queryParameters = "";

        if ($scope.donorCode != undefined && $scope.donorCode > 0) {
            if (queryParameters != "") { queryParameters = queryParameters + "&" }
            queryParameters = "Code=" + $scope.donorCode;
        }

        if ($scope.FullName != undefined && $scope.FullName != "") {
            if (queryParameters != "") { queryParameters = queryParameters + "&" }
            queryParameters = queryParameters + "Name=" + $scope.FullName;
        }

        if ($scope.add1 != undefined && $scope.add1 != "") {
            if (queryParameters != "") { queryParameters = queryParameters + "&" }
            queryParameters = queryParameters + "Add1=" + $scope.add1;
        }

        if ($scope.postcode != undefined && $scope.postcode != "") {
            if (queryParameters != "") { queryParameters = queryParameters + "&" }
            queryParameters = queryParameters + "pcode=" + $scope.postcode;
        }

        if ($scope.city != undefined && $scope.city != "") {
            if (queryParameters != "") { queryParameters = queryParameters + "&" }
            queryParameters = queryParameters + "city=" + $scope.city;
        }

        return queryParameters; 

    };

    $scope.go = function (ai) {
        myService.set(ai);
        alert("I am donor: " + ai.CODE);
    };

}

function OtherController($scope) {
    $scope.pageChangeHandler = function (num) {
        console.log('going to page ' + num);
    };
}

function AllocationController($scope,myService) {
    $scope.donor = myService.get();
}


myApp.factory('myService', function () {
    var savedData = {}
    function set(data) {
        savedData = data;
    }
    function get() {
        return savedData;
    }

    return {
        set: set,
        get: get
    }

});

myApp.controller('ModalInstanceCtrl',ModalInstanceCtrl);
myApp.controller('userController', userController);
myApp.controller('OtherController', OtherController);
myApp.controller('AllocationController', AllocationController);