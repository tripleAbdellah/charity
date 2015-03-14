function AllocationController($scope, $http) {

    $scope.allocation = {};

    $scope.getShoppingCategoryList = function () {
        $scope.loading = true;
        $scope.shoppingListItems = {};
        $http(
            {
                url: "/api/allocation/GetShoppingCategoryList",
                method: "GET",
                headers: { 'Content-Type': 'application/json' }
            }
            ).success(function (data, status, headers, config) {
                $scope.shoppingCategoryList = data;

                $scope.loading = false;
            }
            ).error(function (data, status, headers, config) {
                $scope.status = 'Error occurred: ' + status + ' ' + headers;
                $scope.loading = false;
            }
        );
    }

    $scope.invoice = {
        items: []
    };

    $scope.addShoppingItem = function (shoppingItem, shoppingItemDetail, subqty, subcost) {
        $scope.invoice.items.push({
            qty: subqty,
            unitPrice: shoppingItemDetail.unitPrice,
            itemName: shoppingItem.name,
            subitemName: shoppingItemDetail.unitName,
            cost: subcost
        });
    },


    $scope.removeItem = function (index) {
        $scope.invoice.items.splice(index, 1);
    }

    $scope.allocation.subqdty = 1; 

    $scope.setSubQtyDefault = function () {
        $scope.allocation.subqty = 1;
    }
   
    $scope.calculateSubcost = function () {
        if (parseInt($scope.allocation.subqty) > 0 && angular.isUndefined($scope.allocation.shoppingSubCategoryList.unitPrice) == false) {
            $scope.allocation.subcost = $scope.allocation.subqty * $scope.allocation.shoppingSubCategoryList.unitPrice;
        }
        else {
            $scope.allocation.subcost = 0;
        }
    }

    $scope.total = function () {
        var total = 0;
        angular.forEach($scope.invoice.items, function (item) {
            total += item.qty * item.unitPrice;
        })

        return total;
    }
    

}

DonorApp.controller('AllocationController', AllocationController);