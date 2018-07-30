//Facebook Report Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('FbRepController', FbRepController);

    FbRepController.$inject = ['$scope', 'fbService', '$window', '$cookies'];

    function FbRepController($scope, fbService, $window, $cookies) {

        var vm = this;
        vm.$scope = $scope;
        vm.fbService = fbService;
        vm.$onInit = _init;
        vm.$window = $window;
        vm.$cookies = $cookies;
        vm.report = {};
        vm.report2 = {};
        vm.profile = {};
        vm.grade;
        vm.data_score = 3;
        vm.classColor = _classColor;
        vm.gradeQuote = "";
        vm.topTen = [];
        var isoCountries = {
            'AF': 'Afghanistan',
            'AX': 'Aland Islands',
            'AL': 'Albania',
            'DZ': 'Algeria',
            'AS': 'American Samoa',
            'AD': 'Andorra',
            'AO': 'Angola',
            'AI': 'Anguilla',
            'AQ': 'Antarctica',
            'AG': 'Antigua And Barbuda',
            'AR': 'Argentina',
            'AM': 'Armenia',
            'AW': 'Aruba',
            'AU': 'Australia',
            'AT': 'Austria',
            'AZ': 'Azerbaijan',
            'BS': 'Bahamas',
            'BH': 'Bahrain',
            'BD': 'Bangladesh',
            'BB': 'Barbados',
            'BY': 'Belarus',
            'BE': 'Belgium',
            'BZ': 'Belize',
            'BJ': 'Benin',
            'BM': 'Bermuda',
            'BT': 'Bhutan',
            'BO': 'Bolivia',
            'BA': 'Bosnia And Herzegovina',
            'BW': 'Botswana',
            'BV': 'Bouvet Island',
            'BR': 'Brazil',
            'IO': 'British Indian Ocean Territory',
            'BN': 'Brunei Darussalam',
            'BG': 'Bulgaria',
            'BF': 'Burkina Faso',
            'BI': 'Burundi',
            'KH': 'Cambodia',
            'CM': 'Cameroon',
            'CA': 'Canada',
            'CV': 'Cape Verde',
            'KY': 'Cayman Islands',
            'CF': 'Central African Republic',
            'TD': 'Chad',
            'CL': 'Chile',
            'CN': 'China',
            'CX': 'Christmas Island',
            'CC': 'Cocos (Keeling) Islands',
            'CO': 'Colombia',
            'KM': 'Comoros',
            'CG': 'Congo',
            'CD': 'Congo, Democratic Republic',
            'CK': 'Cook Islands',
            'CR': 'Costa Rica',
            'CI': 'Cote D\'Ivoire',
            'HR': 'Croatia',
            'CU': 'Cuba',
            'CY': 'Cyprus',
            'CZ': 'Czech Republic',
            'DK': 'Denmark',
            'DJ': 'Djibouti',
            'DM': 'Dominica',
            'DO': 'Dominican Republic',
            'EC': 'Ecuador',
            'EG': 'Egypt',
            'SV': 'El Salvador',
            'GQ': 'Equatorial Guinea',
            'ER': 'Eritrea',
            'EE': 'Estonia',
            'ET': 'Ethiopia',
            'FK': 'Falkland Islands (Malvinas)',
            'FO': 'Faroe Islands',
            'FJ': 'Fiji',
            'FI': 'Finland',
            'FR': 'France',
            'GF': 'French Guiana',
            'PF': 'French Polynesia',
            'TF': 'French Southern Territories',
            'GA': 'Gabon',
            'GM': 'Gambia',
            'GE': 'Georgia',
            'DE': 'Germany',
            'GH': 'Ghana',
            'GI': 'Gibraltar',
            'GR': 'Greece',
            'GL': 'Greenland',
            'GD': 'Grenada',
            'GP': 'Guadeloupe',
            'GU': 'Guam',
            'GT': 'Guatemala',
            'GG': 'Guernsey',
            'GN': 'Guinea',
            'GW': 'Guinea-Bissau',
            'GY': 'Guyana',
            'HT': 'Haiti',
            'HM': 'Heard Island & Mcdonald Islands',
            'VA': 'Holy See (Vatican City State)',
            'HN': 'Honduras',
            'HK': 'Hong Kong',
            'HU': 'Hungary',
            'IS': 'Iceland',
            'IN': 'India',
            'ID': 'Indonesia',
            'IR': 'Iran, Islamic Republic Of',
            'IQ': 'Iraq',
            'IE': 'Ireland',
            'IM': 'Isle Of Man',
            'IL': 'Israel',
            'IT': 'Italy',
            'JM': 'Jamaica',
            'JP': 'Japan',
            'JE': 'Jersey',
            'JO': 'Jordan',
            'KZ': 'Kazakhstan',
            'KE': 'Kenya',
            'KI': 'Kiribati',
            'KR': 'Korea',
            'KW': 'Kuwait',
            'KG': 'Kyrgyzstan',
            'LA': 'Lao People\'s Democratic Republic',
            'LV': 'Latvia',
            'LB': 'Lebanon',
            'LS': 'Lesotho',
            'LR': 'Liberia',
            'LY': 'Libyan Arab Jamahiriya',
            'LI': 'Liechtenstein',
            'LT': 'Lithuania',
            'LU': 'Luxembourg',
            'MO': 'Macao',
            'MK': 'Macedonia',
            'MG': 'Madagascar',
            'MW': 'Malawi',
            'MY': 'Malaysia',
            'MV': 'Maldives',
            'ML': 'Mali',
            'MT': 'Malta',
            'MH': 'Marshall Islands',
            'MQ': 'Martinique',
            'MR': 'Mauritania',
            'MU': 'Mauritius',
            'YT': 'Mayotte',
            'MX': 'Mexico',
            'FM': 'Micronesia, Federated States Of',
            'MD': 'Moldova',
            'MC': 'Monaco',
            'MN': 'Mongolia',
            'ME': 'Montenegro',
            'MS': 'Montserrat',
            'MA': 'Morocco',
            'MZ': 'Mozambique',
            'MM': 'Myanmar',
            'NA': 'Namibia',
            'NR': 'Nauru',
            'NP': 'Nepal',
            'NL': 'Netherlands',
            'AN': 'Netherlands Antilles',
            'NC': 'New Caledonia',
            'NZ': 'New Zealand',
            'NI': 'Nicaragua',
            'NE': 'Niger',
            'NG': 'Nigeria',
            'NU': 'Niue',
            'NF': 'Norfolk Island',
            'MP': 'Northern Mariana Islands',
            'NO': 'Norway',
            'OM': 'Oman',
            'PK': 'Pakistan',
            'PW': 'Palau',
            'PS': 'Palestinian Territory, Occupied',
            'PA': 'Panama',
            'PG': 'Papua New Guinea',
            'PY': 'Paraguay',
            'PE': 'Peru',
            'PH': 'Philippines',
            'PN': 'Pitcairn',
            'PL': 'Poland',
            'PT': 'Portugal',
            'PR': 'Puerto Rico',
            'QA': 'Qatar',
            'RE': 'Reunion',
            'RO': 'Romania',
            'RU': 'Russian Federation',
            'RW': 'Rwanda',
            'BL': 'Saint Barthelemy',
            'SH': 'Saint Helena',
            'KN': 'Saint Kitts And Nevis',
            'LC': 'Saint Lucia',
            'MF': 'Saint Martin',
            'PM': 'Saint Pierre And Miquelon',
            'VC': 'Saint Vincent And Grenadines',
            'WS': 'Samoa',
            'SM': 'San Marino',
            'ST': 'Sao Tome And Principe',
            'SA': 'Saudi Arabia',
            'SN': 'Senegal',
            'RS': 'Serbia',
            'SC': 'Seychelles',
            'SL': 'Sierra Leone',
            'SG': 'Singapore',
            'SK': 'Slovakia',
            'SI': 'Slovenia',
            'SB': 'Solomon Islands',
            'SO': 'Somalia',
            'ZA': 'South Africa',
            'GS': 'South Georgia And Sandwich Isl.',
            'ES': 'Spain',
            'LK': 'Sri Lanka',
            'SD': 'Sudan',
            'SR': 'Suriname',
            'SJ': 'Svalbard And Jan Mayen',
            'SZ': 'Swaziland',
            'SE': 'Sweden',
            'CH': 'Switzerland',
            'SY': 'Syrian Arab Republic',
            'TW': 'Taiwan',
            'TJ': 'Tajikistan',
            'TZ': 'Tanzania',
            'TH': 'Thailand',
            'TL': 'Timor-Leste',
            'TG': 'Togo',
            'TK': 'Tokelau',
            'TO': 'Tonga',
            'TT': 'Trinidad And Tobago',
            'TN': 'Tunisia',
            'TR': 'Turkey',
            'TM': 'Turkmenistan',
            'TC': 'Turks And Caicos Islands',
            'TV': 'Tuvalu',
            'UG': 'Uganda',
            'UA': 'Ukraine',
            'AE': 'United Arab Emirates',
            'GB': 'United Kingdom',
            'US': 'United States',
            'UM': 'United States Outlying Islands',
            'UY': 'Uruguay',
            'UZ': 'Uzbekistan',
            'VU': 'Vanuatu',
            'VE': 'Venezuela',
            'VN': 'Viet Nam',
            'VG': 'Virgin Islands, British',
            'VI': 'Virgin Islands, U.S.',
            'WF': 'Wallis And Futuna',
            'EH': 'Western Sahara',
            'YE': 'Yemen',
            'ZM': 'Zambia',
            'ZW': 'Zimbabwe'
        };
        vm.regBtn = _regBtn;
        vm.moreBtn = _moreBtn;
        vm.homeBtn = _homeBtn;
        vm.suggestion = {};

        function _init() {        
            vm.profile = vm.$cookies.getObject('profile');
            vm.report = vm.$cookies.getObject('report');
            vm.report2 = vm.$cookies.getObject('report2');

            vm.grade = vm.report.engagement.score + vm.report2.growth.points + vm.report.activity.points + vm.data_score + vm.report2.reach.score;
            if (vm.grade > 79) {
                vm.gradeQuote = "Good! Let's see where we can improve."
            } else if (vm.grade > 59) {
                vm.gradeQuote = "Fair! Let's see where we can improve.";
            } else {
                vm.gradeQuote = "Not Good! Let's see where we can improve.";
            }

            getSuggestion();

            var countriesBgColors1 = [];
            countriesBgColors1.push(vm.report2.reach.country_Talk_Current);

            var sortable = [];           
            for (var country in vm.report2.reach.country_Talk_Current) {
                sortable.push([country, vm.report2.reach.country_Talk_Current[country]]);
            }
            sortable.sort(function (a, b) {
                return b[1] - a[1];
            });
            for (var i = 0; i < sortable.length; i++) {
                if (i == 10) { break;}
                vm.topTen.push(sortable[i]);
            }
            for (var i = 0; i < sortable.length; i++) {
                if (i == 10) { break; }
                var country = vm.topTen[i];
                var code = getCountryName(country[0].toUpperCase());
                country.push(code);
            }
            countriesBgColors1 = countriesBgColors1.map(function (item) {
                for (var key in item) {
                    item[key.toUpperCase()] = item[key];
                    delete item[key];
                }
                return item;
            })           
            // Init SVG map
            $.HSCore.components.HSSvgMap.init('#vmap', {
                series: {
                    regions: [{
                        scale: ['#C8EEFF', '#0071A4'],
                        normalizeFunction: 'polynomial',
                        values: countriesBgColors1[0]
                    }]
                }
            });
            return;
        }
        function getSuggestion() {
            if (vm.report.engagement.score < 16) {
                vm.suggestion.engagement = 'Try to provide more value for your audience in your posts.';
            } else if (vm.report.engagement.score < 23){
                vm.suggestion.engagement = 'Use gamification to incentivize your audience to invite their friends.'
            } else {
                vm.suggestion.engagement = 'Great!'
            }
            if (vm.report.activity.points < 16) {
                vm.suggestion.activity = 'Try to post more often.';
            } else {
                vm.suggestion.activity = 'Good!'
            }
            if (vm.report2.growth.points < 25) {
                vm.suggestion.growth = 'Offer prizes and incentives that can be shared on social media by your audience.';
            } else {
                vm.suggestion.growth = 'Good!'
            }
            if (vm.report2.reach.score < 25) {
                vm.suggestion.reach = 'Limited reach is a known challenge on social media.';
            } else {
                vm.suggestion.reach = 'Good!'
            }
            vm.suggestion.data = 'Use a 3rd party to collect users actionable data.';
        }
        function getCountryName(countryCode) {
            if (isoCountries.hasOwnProperty(countryCode)) {
                return isoCountries[countryCode];
            } else {
                return countryCode;
            }
        }
        function _classColor(num) {
            if (num>69){
                return 'g-color-green';
            } else if (num>49){
                return 'g-color-yellow';
            } else if(num>29) {
                return 'g-color-orange';
            } else {
                return 'g-color-google-plus';
            }
        }
        function _regBtn() {
            $window.location.href = "/home/register";
        }
        function _moreBtn() {
            $window.location.href = "http://www.memeni.com/";
        }
        function _homeBtn() {
            $window.location.href = "/";
        }
    }
})();

//Activity Chart
(function () {
    angular
        .module("mainApp")
        .controller("BarCtrl", function ($scope, $cookies) {

        this.$onInit = _init;
        $scope.labels = [];
        $scope.series = ['Posts'];

        $scope.data = [[]];

        function _init() {
            var report = $cookies.getObject('report');
            var weekReport = report.engagement.week_Stats;
            for (var i = weekReport.length - 1; i > -1; i--) {
                $scope.labels.push(weekReport[i].dateString);
                $scope.data[0].push(weekReport[i].posts);
            }
            $scope.options = {
                scales: {
                    yAxes: [{
                        display: true,
                        ticks: {
                            suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                            // OR //
                        }
                    }]
                }
            };
            return;
        }
    });
})();

//Engagement Chart
(function () {
    angular
        .module("mainApp")
        .controller("LineCtrl", function ($scope, $cookies) {

        this.$onInit = _init;
        $scope.labels = [];
        $scope.series = ['Reactions', 'Likes', 'Comments', 'Shares'];
        $scope.data = [[],[],[],[]];
        //$scope.onClick = function (points, evt) {
        //    console.log(points, evt);
        //};
        $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }, { yAxisID: 'y-axis-2' }];
        $scope.options = {
            scales: {
                yAxes: [
                    {
                        id: 'y-axis-1',
                        type: 'linear',
                        display: true,
                        position: 'left'
                    },
                    {
                        id: 'y-axis-2',
                        type: 'linear',
                        display: true,
                        position: 'right'
                    }
                ]
            }
        };

        function _init() {
                var report = $cookies.getObject('report');
                var weekReport = report.engagement.week_Stats;
                for (var i = weekReport.length -1; i > -1; i--) {
                    $scope.labels.push(weekReport[i].dateString);
                    $scope.data[0].push(weekReport[i].reactions);
                    $scope.data[1].push(weekReport[i].likes);
                    $scope.data[2].push(weekReport[i].comments);
                    $scope.data[3].push(weekReport[i].shares);
                }         
                return;
            }
    });
})();

//Radar Chart
(function () {
    angular
        .module("mainApp")
        .controller("RadarCtrl", function ($scope, $cookies) {

        $scope.labels = ["Activity", "Engagement", "Growth", "Reach", "Data Collection"];
        $scope.data = [];
        $scope.options = {
            scale: {
                ticks: {
                    beginAtZero: true,
                    min: 0,
                    max: 100,
                    stepSize: 20
                }
            }
        };
        var report = $cookies.getObject('report');
        var report2 = $cookies.getObject('report2');
        $scope.data.push(Math.ceil(report.activity.points / 25 * 100));
        $scope.data.push(Math.ceil(report.engagement.score / 25 * 100));
        $scope.data.push(Math.ceil(report2.growth.points / 25 * 100));
        $scope.data.push(Math.ceil(report2.reach.score / 10 * 100));
        $scope.data.push(Math.ceil(3 / 15 * 100));
        })
})();