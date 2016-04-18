(function () {
    'use strict';

    angular.module('common').service('cacheManager', ['$cacheFactory', cacheManager]);

    // Create cache 
    function cacheManager(cacheFactory) {

        cacheFactory('dataCache', {
            maxAge: 15 * 60 * 1000, // Items added to this cache expire after 15 minutes
            cacheFlushInterval: 60 * 60 * 1000, // This cache will clear itself every hour
            deleteOnExpire: 'aggressive' // Items will be deleted from this cache when they expire
        });

        // Get item from cache 
        function getItem(key) {
            var dataCache = cacheFactory.get('dataCache');

            if (dataCache.get(key)) {
                return (dataCache.get(key));
            }
            //else {
            //    throw new Exception("Cache item is not supported");
            //}
        }

        // Set item in cahce
        function setItem(key, value) {
            var dataCache = cacheFactory.get('dataCache');
            dataCache.put(key, value);

        }

        return {
            getItem: getItem,
            setItem: setItem
        };
    }

})();