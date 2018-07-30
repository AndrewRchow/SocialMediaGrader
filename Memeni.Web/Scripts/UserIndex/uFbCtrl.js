//Facebook Controller
(function () {
    "use strict";
    angular
        .module('mainApp')
        .controller('userFbController', userFbController);

    userFbController.$inject = ['$scope', 'genericService', '$window', 'toastr'];

    function userFbController($scope, genericService, $window, toastr) {

        var vm = this;
        vm.$scope = $scope;
        vm.genericService = genericService;
        vm.$onInit = _init;
        vm.$window = $window;
        vm.report = {};
        vm.fbName = "";
        vm.twName = "";
        vm.fbAcct = false;
        vm.btnLoad = false;
        vm.loading = false;
        vm.fbDash = false;
        vm.submitFb = _submitFb;
        vm.userId;
        vm.userEmail;
        vm.firstName;
        vm.enTable = [];
        vm.avgDpst = 0;
        vm.totalPst = 0;
        vm.topTenG = [];
        vm.topTenR = [];
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
        vm.fbProfileInfo;
        vm.resetLink = _resetLink;
        vm.reset = false;
        vm.fSwitch = false;
        vm.tSwitch = false;
        vm.switch = _switch;
        vm.twitterSwitch = _twitterSwitch;
        vm.toastr = toastr;

        function _init() {          
            vm.loading = true;            
            return;
        }

        $scope.$on('userId', function (event, data) {
            vm.userId = data;
            vm.genericService.getById('/api/user/fb/', data)
                .then(_getFbGood, _getFbError);
        });

        $scope.$on('userEmail', function (event, data) {
            vm.userEmail = data;
        });

        $scope.$on('firstName', function (event, data) {
            vm.firstName = data;
        });

        //
        $scope.$on('twitterSwitch', function (event, data) {
            vm.twitterSwitch(data);
        });
        
        function _getFbGood(resp) {
            //receive settings info on logged in user and 
            console.log(resp.data);
            vm.fSwitch = resp.data.wklyFb;
            vm.tSwitch = resp.data.wklyTwt;
            vm.fbName = resp.data.facebook;
            vm.twName = resp.data.twitter;
            if (!resp.data.facebook) {
                vm.loading = false;
                vm.twName = "";
                return vm.fbAcct = true;
            } else {
                vm.reset = true;
                vm.genericService.postById('/api/user/fb/posts/', resp.data.facebook)
                .then(_dashboardPostsGood, _error);
                vm.genericService.getById('/api/fb/profile/', vm.fbName)
                .then(_fbProfileInfoGood, _error); 
            }
        }
        function _submitFb(name) {
            //creates new profile for user to link to fb, twitter, email services settings
            vm.genericService.getById('/api/user/fb/profile/', name)
                .then(_fbProfileGood, _fbProfileError);
            vm.btnLoad = true;
        }
        function _fbProfileGood(resp) {
            var userDashData = {};
            userDashData.Id = vm.userId;
            userDashData.Facebook = resp.data.id;
            vm.fbName = resp.data.id;
            console.log('fb profile good', resp.data);
            userDashData.Twitter = vm.twName;
            userDashData.WeeklyFB = vm.fSwitch;
            userDashData.WeeklyTwitter = vm.tSwitch;
            //sets values for user dashboard profile that was successfully set up
            vm.genericService.post('/api/user/fb/dashboard/', userDashData)
                .then(_userDashboardGood, _error);
            vm.genericService.getById('/api/user/fb/profile/', vm.fbName)
                .then(_fbProfileInfoGood, _error); 
            _facebookSwitch(userDashData);
        }
        function _fbProfileInfoGood(resp) {
            vm.fbProfileInfo = resp.data;
            return;
        }
        function _fbProfileInfoGood2(resp) {
            if (resp.data.is_verified == false) {
                vm.btnLoad = false;
                return vm.toastr.error("This Account is Not Verified. Please try another one.");
            } else {
                return vm.uFbService.fbProfile(resp.data.id)
                    .then(_fbProfileGood, _fbProfileError);
            }
        }
        function _userDashboardGood(resp) {
            if (vm.reset == true) {
                return $window.location.reload();
            }
            //calls for entry of last 30d posts, updates if previous records
            vm.genericService.postById('/api/user/fb/posts/', vm.fbName)
                .then(_dashboardPostsGood, _error);
        }
        function _dashboardPostsGood(resp) {
            $scope.$broadcast('fbDashboard', resp.data);
            var totalRxn = 0, totalLike = 0, totalComm = 0, totalShr = 0, totalPst = 0;
            for (var i = 0; i < resp.data.length; i++) {
                totalRxn += resp.data[i].reactions;
                totalLike += resp.data[i].likes;
                totalComm += resp.data[i].comments;
                totalShr += resp.data[i].shares;
                totalPst += resp.data[i].posts;
            };

            var avgDrxn = totalRxn / resp.data.length;
            var avgDlike = totalLike / resp.data.length;
            var avgDcomm = totalComm / resp.data.length;
            var avgDshr = totalShr / resp.data.length;
            vm.avgDpst = totalPst / resp.data.length;
            vm.totalPst = totalPst;

            var avgPrxn = totalRxn / totalPst;
            var avgPlike = totalLike / totalPst;
            var avgPcomm = totalComm / totalPst;
            var avgPshr = totalShr / totalPst;

            vm.enTable.push(['Reactions', avgDrxn, avgPrxn, totalRxn]);
            vm.enTable.push(['Likes', avgDlike, avgPlike, totalLike]);
            vm.enTable.push(['Comments', avgDcomm, avgPcomm, totalComm]);
            vm.enTable.push(['Shares', avgDshr, avgPshr, totalShr]);

            vm.loading = false;
            vm.fbAcct = false;
            vm.fbDash = true;
            vm.genericService.getById('/api/user/fb/growth/', vm.fbName)
                .then(_growthGood, _error);
            vm.genericService.getById('/api/user/fb/reach/', vm.fbName)
                .then(_reachGood, _error);
            return;
        }
        function _growthGood(resp) {
            var countriesBgColors1 = [];
            countriesBgColors1.push(resp.data.current_Value);
            countriesBgColors1 = countriesBgColors1.map(function (item) {
                for (var key in item) {
                    item[key.toUpperCase()] = item[key];
                    delete item[key];
                }
                return item;
            });  
            //var countries2 = resp.data.past_Value;
            var sortable = [];
            for (var country in resp.data.current_Value) {
                sortable.push([country, resp.data.current_Value[country]]);
            }
            sortable.sort(function (a, b) {
                return b[1] - a[1];
            });
            for (var i = 0; i < sortable.length; i++) {
                if (i == 10) { break;}
                vm.topTenG.push(sortable[i]);
                var country = vm.topTenG[i];
                var code = getCountryName(country[0].toUpperCase());
                country.push(code);
            }
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
        function _reachGood(resp) {
            var countriesBgColors1 = [];
            countriesBgColors1.push(resp.data.country_Talk_Current);
            //var countries2 = resp.data.country_Talk_Month_Ago;
            var sortable = [];
            for (var country in resp.data.country_Talk_Current) {
                sortable.push([country, resp.data.country_Talk_Current[country]]);
            }
            sortable.sort(function (a, b) {
                return b[1] - a[1];
            });
            for (var i = 0; i < sortable.length; i++) {
                if (i == 10) { break; }
                vm.topTenR.push(sortable[i]);
                var country = vm.topTenR[i];
                var code = getCountryName(country[0].toUpperCase());
                country.push(code);
            }
            return;
        }
        function _error(error) {
            console.log(error);
        }
        function _fbProfileError(error) {
            vm.btnLoad = false;
            console.log(error);
            return vm.toastr.error("This Account is Not Valid. Please try another one.");
        }
        function _fbProfileError(error) {
            vm.btnLoad = false;
            return vm.toastr.error("This Account is Not Verified. Please try another one.");
        }
        function _getFbError(error) {
            vm.loading = false;
            vm.fbAcct = true;
        }
        function getCountryName(countryCode) {
            if (isoCountries.hasOwnProperty(countryCode)) {
                return isoCountries[countryCode];
            } else {
                return countryCode;
            }
        }
        function _resetLink() {
            vm.fbDash = false;
            vm.fbAcct = true;
        }
        function _switch(data) {
            var userDashData = {};
            userDashData.Id = vm.userId;
            userDashData.Facebook = vm.fbName;
            userDashData.Twitter = vm.twName;
            userDashData.WeeklyFB = vm.fSwitch;
            userDashData.WeeklyTwitter = vm.tSwitch;
            //sets values for user dashboard profile that was successfully set up
            vm.genericService.post('/api/user/fb/dashboard/', userDashData)
                .then(_switchGood, _error);
            
            if (data === "f") {
                _facebookSwitch(userDashData);
            } else if (data === "t") {
                _twitterSwitch(userDashData);
            }
            return;
        }
        function _switchGood() {
            return vm.toastr.success("Email Settings Successfully Changed!");
        }

        function _facebookSwitch(userDashData) {
            if (vm.fSwitch == true) {
                var data = { "Id": "f" + vm.userId, "Email": vm.userEmail, "Name": userDashData.Facebook, "Url": "facebook", "FirstName": vm.firstName }; 
                vm.genericService.postById("/api/email/weeklyreport/facebook/", userDashData.Facebook, data)
                    .then(_switchSuccess)
                    .catch(_switchFailure);

            } else if (vm.fSwitch == false) {
                vm.genericService.postById("/api/email/cancelreport/", "f" + vm.userId)
                    .then(_switchSuccess)
                    .catch(_switchFailure);
            }
        }

        // function i want to call
        function _twitterSwitch(userDashData) {
            if (vm.tSwitch == true) {
                var data = vm.switchTwitter = { "Id": "t" + vm.userId, "Email": vm.userEmail, "Name": userDashData.Twitter, "Url": "twitter", "FirstName": vm.firstName }; 
                console.log(data);
                vm.genericService.postById("/api/email/weeklyreport/twitter/", userDashData.Twitter, data)
                    .then(_switchSuccess)
                    .catch(_switchFailure);

            } else if (vm.tSwitch === false) {
                vm.genericService.postById("/api/email/cancelreport/", "t" + vm.userId)
                    .then(_switchSuccess)
                    .catch(_switchFailure);
            }
        }

        function _switchSuccess() {
            return toastr.success("Switch success");
        }

        function _switchFailure() {
            return toastr.error("Switch error");
        }
    }
})();

//Activity Chart
(function () {
    angular
        .module("mainApp")
        .controller("BarCtrl", function ($scope, $cookies) {

            $scope.labels = [];
            $scope.series = ['Posts'];

            $scope.data = [[]];

            $scope.options = {
                scales: {
                    yAxes: [{
                        display: true,
                        ticks: {
                            suggestedMin: 0,    // minimum will be 0, unless there is a lower value.
                        }
                    }]
                }
            };
            $scope.$on('fbDashboard', function (event, data) {
                var report = data;
                for (var i = report.length - 1; i > -1; i--) {
                    $scope.labels.push(report[i].dateString);
                    $scope.data[0].push(report[i].posts);
                };
            });
            return;
        });
})();
//Engagement Chart
(function () {
    angular
        .module("mainApp")
        .controller("LineCtrl", function ($scope, $cookies) {

            $scope.labels = [];
            $scope.series = ['Reactions', 'Likes', 'Comments', 'Shares'];
            $scope.data = [[], [], [], []];
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
            $scope.$on('fbDashboard', function (event, data) {
                var report = data;
                for (var i = report.length - 1; i > -1; i--) {
                    $scope.labels.push(report[i].dateString);
                    $scope.data[0].push(report[i].reactions);
                    $scope.data[1].push(report[i].likes);
                    $scope.data[2].push(report[i].comments);
                    $scope.data[3].push(report[i].shares);
                };
            });
        });
})();

