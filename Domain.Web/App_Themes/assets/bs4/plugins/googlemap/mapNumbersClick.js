
function GetNumbersMap(_url, divID) {
    $.ajax({
        url: _url,
        data: {},
        cache: false,
        type: "POST",
        success: function (data) {
            initMapNumbers(data, divID);
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}

function initMapNumbers(citiesTmp, divID) {

    var selectedRegion = 0;
    var cities = [];
    var data = [];
    var minCity = 0;
    var maxCity = 0;
    var minRegion = 0;
    var maxRegion = 0;
    var map = null;
    var regions = [0, 0, 0, 0, 0, 0, 0];
    var allMarkers = [];

    citiesTmp = citiesTmp.sort(function (a, b) { return parseInt(a.key) - parseInt(b.key); });
    var counter = 0;
    var tmp;

    if (citiesTmp.length == 0) {
        for (j = 0; j < 81 ; j++) {
            tmp = { "key": (j + 1).toString(), "value": "0" };
            cities.push(tmp);
        }
    }
    else {
        if (parseInt(citiesTmp[0].key.toString()) != 1) {
            for (j = 0; j < parseInt(citiesTmp[0].key.toString()) - 1 ; j++) {
                tmp = { "key": (j + 1).toString(), "value": "0" };
                cities.push(tmp);
            }
        }
    }

    for (i = 0; i < citiesTmp.length; i++) {
        tmp = { "key": citiesTmp[i].key, "value": citiesTmp[i].value };
        cities.push(tmp);
        counter = parseInt(citiesTmp[i].key);
        if (i < citiesTmp.length - 1) {
            var range = parseInt(citiesTmp[i + 1].key.toString()) - parseInt(citiesTmp[i].key.toString());
            for (j = 0; j < range - 1; j++) {
                counter = counter + 1;
                var tmp = { "key": counter.toString(), "value": "0" };
                cities.push(tmp);
            }
        }
    }
    for (j = parseInt(cities[cities.length - 1].key.toString()) ; j < 81; j++) {
        tmp = { "key": (j + 1).toString(), "value": "0" };
        cities.push(tmp);
    }
    for (i = 0; i < cities.length; i++) {
        switch (parseInt(citiesMarker[i].properties.region)) {
            case 1:
                regions[0] += parseInt(cities[i].value);
                break;
            case 2:
                regions[1] += parseInt(cities[i].value);
                break;
            case 3:
                regions[2] += parseInt(cities[i].value);
                break;
            case 4:
                regions[3] += parseInt(cities[i].value);
                break;
            case 5:
                regions[4] += parseInt(cities[i].value);
                break;
            case 6:
                regions[5] += parseInt(cities[i].value);
                break;
            case 7:
                regions[6] += parseInt(cities[i].value);
                break;
        }
    }

    //harita tanımlanıyor
    map = new google.maps.Map(document.getElementById(divID), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 6,
        center: { lat: 39.0311652, lng: 35.6385272 }
    });
    data = map.data.addGeoJson(regionsPolygon);

    //markerları yönetmek için markermanager oluşturuluyor
    mgr = new MarkerManager(map);
    google.maps.event.addListener(mgr, 'loaded', function () {
        createMarker(regionsMarker, true);
    });
    
    //haritanın click olayı tanımlanıyor
    map.data.addListener('click', function (e) {
        
        if (selectedRegion == 0) {
            if (data != null || data != undefined) {
                for (var i = 0; i < data.length; i++) {
                    map.data.remove(data[i]);
                }
            }
            selectedRegion = e.feature.l.description;
            data = map.data.addGeoJson(citiesPolygon);

            var citiesMarkerTmp = [];
            for (i = 0; i < 81; i++)
            {
                if(citiesMarker[i].properties.region == parseInt(selectedRegion))
                {
                    citiesMarkerTmp.push(citiesMarker[i]);
                }
            }

            for (i = 0; i < allMarkers.length; i++)
            {
                allMarkers[i].setMap(null);
            }
 
            mgr.refresh();
            createMarker(citiesMarkerTmp, false);
        }
    });

    //markerları sıfırlayan fonksiyon
    function clearMarkers() {
        mgr.clearMarkers();
        updateStatus(mgr.getMarkerCount(map.getZoom()));
    }

    //markerları oluşturan fonksiyon
    function createMarker(markerList, region) {

        allMarkers = [];

        for (i = 0; i < markerList.length; i++) {
            var latLng = new google.maps.LatLng(parseFloat(markerList[i].geometry.coordinates[0]), parseFloat(markerList[i].geometry.coordinates[1]));
            //var marker = new google.maps.Marker({ position: latLng, weight: cities[i].value.toString(), label: cities[i].value.toString(), icon: "../Content/libs/googleMap/m2.png" });

            if (region) {
                var marker1 = new MarkerWithLabel({
                    position: latLng,
                    draggable: false,
                    raiseOnDrag: false,
                    icon: "/App_Themes/assets/bs4/plugins/googleMap/m3.png",
                    map: map,
                    labelContent: regions[i].toString(),
                    labelAnchor: new google.maps.Point(3, 38),
                    labelClass: "labels2",
                    labelStyle: { opacity: 1 }
                });

                mgr.addMarker(marker1);
                allMarkers.push(marker1);
            }
            else {

                var ilID = parseInt(markerList[i].properties.description);

                var marker1 = new MarkerWithLabel({
                    position: latLng,
                    draggable: false,
                    raiseOnDrag: false,
                    icon: "/App_Themes/assets/bs4/plugins/googleMap/m2.png",
                    map: map,
                    labelContent: cities[ilID-1].value.toString(),
                    labelAnchor: new google.maps.Point(3, 33),
                    labelClass: "labels",
                    labelStyle: { opacity: 1 }
                });
                mgr.addMarker(marker1);
                allMarkers.push(marker1);
            }
        }
    }

    map.data.setStyle(styleFeature);

    function styleFeature(feature) {
        var value = 0;
        var min = 0;
        var max = 0;
        var visibleOption = false;

        if (selectedRegion == 0) {
            visibleOption = true;
            min = minRegion;
            max = maxRegion;
            value = regions[parseInt(feature.getProperty('description')) - 1];
        }
        else {
            min = minCity;
            max = maxCity;
            value = parseInt(cities[parseInt(feature.getProperty('description')) - 1].value);
            if (citiesPolygon.features[parseInt(feature.getProperty('description')) - 1].properties.region == selectedRegion) {
                visibleOption = true;
            }
        }

        var range = (max - min) / 7;
        var renk = "";

        if (min == max) {
            renk = '#800026';
        }
        else {
            if (value >= max)
                renk = '#0c4119';
            else if (value >= (max - range))
                renk = '#439a58';
            else if (value >= (max - (range * 2)))
                renk = '#93d7a4';
            else if (value >= (max - (range * 3)))
                renk = '#93d7a4';
            else if (value >= (max - (range * 4)))
                renk = '#FED976';
            else if (value >= (max - (range * 5)))
                renk = '#FD8D3C';
            else if (value >= (max - (range * 6)))
                renk = '#d1436d';
            else
                renk = '#800026';
        }

        return {
            strokeColor: 'black',
            fillColor: 'rgba(78, 78, 78, 0.50)',
            fillOpacity: 0.95,
            visible: visibleOption,
            strokeWeight: 0.5

        };
    }

}