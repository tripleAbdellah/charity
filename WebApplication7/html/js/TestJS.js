function userController($scope, $http) {
    $scope.edit = true;
    $scope.loading = true;

    $scope.reset = function () {
        var donorObj = $scope.jsonResults();
        var timeout = 40000; //40 seconds. 
 
        $scope.testoesafie = donorObj;
        
        $http(
            {
                url: '/api/donor/addDonor',
                method: "POST",
                data: JSON.stringify(donorObj),
                headers: { 'Content-Type': 'application/json' }
            }
            ).success(function (data, status, headers, config) {
                $scope.dataResults = data;
            }
            ).error(function (data, status, headers, config) {
                $scope.status = status + ' ' + headers;
            }
        );
        $scope.loading = false;
    }



    $scope.jsonResults =  function () {
        return {
            'NAME': $scope.FullName,
            'TYPE': $scope.donorType,
            'ADD1': $scope.add1,
            'ADD2': $scope.add2,
            'CITY': $scope.city,
            'PCODE': $scope.postcode,
            'COUNTY': $scope.county,
            'COUNTRY': $scope.country,
            'TEL': $scope.home_tel,
            'TEL_WORK': $scope.work_tel,
            'MOBILE': $scope.mob_tel,
            'TITLE': $scope.TITLE,
            'EMAIL': $scope.email,
            'ERECEIPT': true,
            'GAD': $scope.GAD
        };
    }

}