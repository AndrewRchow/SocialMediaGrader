(function () {
    'use strict';
    angular
        .module('mainApp')
        .controller('adminHomeController', adminHomeController);

    adminHomeController.$inject = ['$scope'];

    function adminHomeController($scope) {

        var vm = this;
        vm.$onInit = _init;


        //====[ THE FOLD ]=======================================

        function _init() {

            new Chart(document.getElementById("mixed-chart"), {
                type: 'bar',
                data: {
                    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"],
                    datasets: [{
                        label: "Page Hits",
                        type: "line",
                        borderColor: "#1fd112",
                        data: [408, 1700, 1202, 1234, 1456, 1765, 1432, 987, 1385, 937, 1456, 1697],
                        fill: false
                    }, {
                        label: "Registrations",
                        type: "line",
                        borderColor: "#0b84e8",
                        data: [120, 670, 354, 450, 561, 675, 342, 254, 502, 578, 450, 724],
                        fill: false
                    }, {
                        label: "Page Hits",
                        type: "bar",
                        backgroundColor: "rgba(31,209,18,0.5)",
                        data: [408, 1700, 1202, 1234, 1456, 1765, 1432, 987, 1385, 937, 1456, 1697],
                    }, {
                        label: "Registrations",
                        type: "bar",
                        backgroundColor: "rgba(11,132,232,0.5)",
                        backgroundColorHover: "#3e95cd",
                        data: [120, 670, 354, 450, 561, 675, 342, 254, 502, 578, 450, 724]
                    }
                    ]
                },
                options: {
                    title: {
                        display: true,
                        text: 'Page Hits vs Registrations'
                    },
                    legend: { display: false }
                }
            });

        }
    }
})();