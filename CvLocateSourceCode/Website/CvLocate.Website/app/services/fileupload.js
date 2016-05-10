(function () {
    'use strict';
    var app = angular.module('app');

    app.directive('fileModel', ['$q','$parse', function ($q,$parse) {
        return {
            restrict: 'A',
            scope: {
                image: '=',
                resizeMaxHeight: '@?',
                resizeMaxWidth: '@?',
                resizeQuality: '@?',
                resizeType: '@?',
            },
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;
                
                var getResizeArea = function () {
                    var resizeAreaId = 'fileupload-resize-area';

                    var resizeArea = document.getElementById(resizeAreaId);

                    if (!resizeArea) {
                        resizeArea = document.createElement('canvas');
                        resizeArea.id = resizeAreaId;
                        resizeArea.style.visibility = 'hidden';
                        document.body.appendChild(resizeArea);
                    }

                    return resizeArea;
                }

                var resizeImage = function (origImage, options) {
                    var maxHeight = options.resizeMaxHeight || 300;
                    var maxWidth = options.resizeMaxWidth || 250;
                    var quality = options.resizeQuality || 0.7;
                    var type = options.resizeType || 'image/jpg';

                    var canvas = getResizeArea();

                    var height = origImage.height;
                    var width = origImage.width;

                    // calculate the width and height, constraining the proportions
                    if (width > height) {
                        if (width > maxWidth) {
                            height = Math.round(height *= maxWidth / width);
                            width = maxWidth;
                        }
                    } else {
                        if (height > maxHeight) {
                            width = Math.round(width *= maxHeight / height);
                            height = maxHeight;
                        }
                    }

                    canvas.width = width;
                    canvas.height = height;

                    //draw image on canvas
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(origImage, 0, 0, width, height);

                    // get the data from canvas as 70% jpg (or specified type).
                    return canvas.toDataURL(type, quality);
                };
                var createImage = function (url, callback) {
                    var image = new Image();
                    image.onload = function () {
                        callback(image);
                    };
                    image.src = url;
                };

                var doResizing = function (imageResult, callback) {
                    createImage(imageResult.url, function (image) {
                        var dataURL = resizeImage(image,scope);
                        imageResult.resized = {
                            dataURL: dataURL,
                            type: dataURL.match(/:(.+\/.+);/)[1],
                        };
                        callback(imageResult);
                    });
                };

                var fileToDataURL = function (file) {
                    var deferred = $q.defer();
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        deferred.resolve(e.target.result);
                    };
                    reader.readAsDataURL(file);
                    return deferred.promise;
                };

                element.bind('change', function onchange(evt) {
                    var files = evt.target.files;
                    //create a result object for each file in files
                    var imageResult = {
                        file: files[0],
                        url: URL.createObjectURL(files[0])
                    };

                    fileToDataURL(files[0]).then(function (dataURL) {
                        imageResult.dataURL = dataURL;
                    });

                    if (scope.resizeMaxHeight || scope.resizeMaxWidth) { //resize image
                        doResizing(imageResult, function (imageResult) {
                            scope.image = imageResult;//applyScope(imageResult);
                        });
                    }
                    else { //no resizing
                        scope.image = imageResult; //applyScope(imageResult);
                    }
                    //scope.$apply(function onchangeApply() {
                       
                    //   // modelSetter(scope, element[0].files[0]);
                    //    var c  = getResizeArea();
                    //    var ctx = c.getContext('2d');
                    //    ctx.clearRect(0, 0, c.width, c.height);
                    //    var img = new Image();
                    //    img.src = URL.createObjectURL(element[0].files[0]);
                    //    img.onload = function () {
                    //        ctx.drawImage(img, 0, 0);
                            
                    //    }
                    //    modelSetter(scope, c.toDataURL());
                    //    scope.vm.client.CompanyLogo = c.toDataURL();
                    //});
                });
            }
        }
    }])
        .service('fileUploadService', ['$http', function ($http) {
            this.uploadFileToUrl = uploadFileToUrl;

            function uploadFileToUrl(file, uploadUrl) {
                var fd = new FormData();
                fd.append('file', file);
                $http.post(uploadUrl, fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                })
                        .success(function () {
                        })
                        .error(function () {
                        });
            }
        }]);
})();



//    var serviceId = 'fileuploadService';
//    angular.module('app').factory(serviceId, ['$http', 'common', fileuploadService]);


//    function fileuploadService($http, common) {

//        var fac = {};
//        fac.UploadFile = function (file) {
//            var formData = new FormData();
//            formData.append("file", file);
//            var defer = $q.defer();
//            $http.post("/Common/UploadFile", formData,
//                {
//                    withCredentials: true,
//                    headers: { 'Content-Type': undefined },
//                    transformRequest: angular.identity
//                })

//            .success(function (d) {
//                defer.resolve(d);
//            })

//            .error(function () {
//                defer.reject("File Upload Failed!");
//            });

//            return defer.promise;
//        }
//    }
//})();


//angular.module('imageResizer', ['imageupload'])

//.config(['$compileProvider', function ($compileProvider) {
//    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|data|blob|chrome-extension):/);
//    $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|ftp|mailto|data|blob|chrome-extension):/);
//}])




//angular.module('app')
//    .directive('image', function ($q) {
//        'use strict'

//        var URL = window.URL || window.webkitURL;

//        var getResizeArea = function () {
//            var resizeAreaId = 'fileupload-resize-area';

//            var resizeArea = document.getElementById(resizeAreaId);

//            if (!resizeArea) {
//                resizeArea = document.createElement('canvas');
//                resizeArea.id = resizeAreaId;
//                resizeArea.style.visibility = 'hidden';
//                document.body.appendChild(resizeArea);
//            }

//            return resizeArea;
//        }

//        var resizeImage = function (origImage, options) {
//            var maxHeight = options.resizeMaxHeight || 300;
//            var maxWidth = options.resizeMaxWidth || 250;
//            var quality = options.resizeQuality || 0.7;
//            var type = options.resizeType || 'image/jpg';

//            var canvas = getResizeArea();

//            var height = origImage.height;
//            var width = origImage.width;

//            // calculate the width and height, constraining the proportions
//            if (width > height) {
//                if (width > maxWidth) {
//                    height = Math.round(height *= maxWidth / width);
//                    width = maxWidth;
//                }
//            } else {
//                if (height > maxHeight) {
//                    width = Math.round(width *= maxHeight / height);
//                    height = maxHeight;
//                }
//            }

//            canvas.width = width;
//            canvas.height = height;

//            //draw image on canvas
//            var ctx = canvas.getContext("2d");
//            ctx.drawImage(origImage, 0, 0, width, height);

//            // get the data from canvas as 70% jpg (or specified type).
//            return canvas.toDataURL(type, quality);
//        };

//        var createImage = function (url, callback) {
//            var image = new Image();
//            image.onload = function () {
//                callback(image);
//            };
//            image.src = url;
//        };

//        var fileToDataURL = function (file) {
//            var deferred = $q.defer();
//            var reader = new FileReader();
//            reader.onload = function (e) {
//                deferred.resolve(e.target.result);
//            };
//            reader.readAsDataURL(file);
//            return deferred.promise;
//        };


//        return {
//            restrict: 'A',
//            scope: {
//                image: '=',
//                resizeMaxHeight: '@?',
//                resizeMaxWidth: '@?',
//                resizeQuality: '@?',
//                resizeType: '@?',
//            },
//            link: function postLink(scope, element, attrs, ctrl) {

//                var doResizing = function (imageResult, callback) {
//                    createImage(imageResult.url, function (image) {
//                        var dataURL = resizeImage(image, scope);
//                        imageResult.resized = {
//                            dataURL: dataURL,
//                            type: dataURL.match(/:(.+\/.+);/)[1],
//                        };
//                        callback(imageResult);
//                    });
//                };

//                var applyScope = function (imageResult) {
//                    scope.$apply(function () {
//                        //console.log(imageResult);
//                        if (attrs.multiple)
//                            scope.image.push(imageResult);
//                        else
//                            scope.image = imageResult;
//                    });
//                };


//                element.bind('change', function (evt) {
//                    //when multiple always return an array of images
//                    if (attrs.multiple)
//                        scope.image = [];

//                    var files = evt.target.files;
//                    for (var i = 0; i < files.length; i++) {
//                        //create a result object for each file in files
//                        var imageResult = {
//                            file: files[i],
//                            url: URL.createObjectURL(files[i])
//                        };

//                        fileToDataURL(files[i]).then(function (dataURL) {
//                            imageResult.dataURL = dataURL;
//                        });

//                        if (scope.resizeMaxHeight || scope.resizeMaxWidth) { //resize image
//                            doResizing(imageResult, function (imageResult) {
//                                applyScope(imageResult);
//                            });
//                        }
//                        else { //no resizing
//                            applyScope(imageResult);
//                        }
//                    }
//                });
//            }
//        };
//    });