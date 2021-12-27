
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

    var cities = [];
    var data = [];
    var map = null;
    var mgr = null;
    var cityMarkerSet = false;
    var regions = [0, 0, 0, 0, 0, 0, 0];

    citiesTmp = citiesTmp.sort(function (a, b) { return parseInt(a.key) - parseInt(b.key); });
    var counter = 0;
    for (i = 0; i < citiesTmp.length; i++) {
        var tmp = { "key": citiesTmp[i].key, "value": citiesTmp[i].value };
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
    for (i = (counter + 1) ; i < 82; i++) {
        var tmp = { "key": i.toString(), "value": "0" };
        cities.push(tmp);
    }
    cities = cities.sort(function (a, b) { return parseInt(a.key) - parseInt(b.key); });

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

    map = new google.maps.Map(document.getElementById(divID), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 6,
        center: { lat: 39.0311652, lng: 35.6385272 }
    });
    data = map.data.addGeoJson(regionsPolygon);

    mgr = new MarkerManager(map);
    google.maps.event.addListener(mgr, 'loaded', function () {
        createMarker(regionsMarker, true);
    });

    map.data.setStyle(styleFeature);
    google.maps.event.addListener(map, 'zoom_changed', function (event) {

        if (data != null || data != undefined) {
            for (var i = 0; i < data.length; i++) {
                map.data.remove(data[i]);
            }
        }
        var zoom = map.getZoom();

        if (zoom < 7) {
            //bölgesel olarak göster
            data = map.data.addGeoJson(regionsPolygon);
        }
        else if (zoom >= 7) {
            //il bazlı göster
            if (cityMarkerSet == false) {
                createMarker(citiesMarker, false);
                cityMarkerSet = true;
            }
            data = map.data.addGeoJson(citiesPolygon);
        }
    });

    function createMarker(markerList, region) {
        for (i = 0; i < markerList.length; i++) {
            var latLng = new google.maps.LatLng(parseFloat(markerList[i].geometry.coordinates[0]), parseFloat(markerList[i].geometry.coordinates[1]));
            //var marker = new google.maps.Marker({ position: latLng, weight: cities[i].value.toString(), label: cities[i].value.toString(), icon: "../Content/libs/googleMap/m2.png" });

            if (region) {
                var marker1 = new MarkerWithLabel({
                    position: latLng,
                    draggable: true,
                    raiseOnDrag: true,
                    icon: "../Content/libs/googleMap/m3.png",
                    map: map,
                    labelContent: regions[i].toString(),
                    labelAnchor: new google.maps.Point(8, 40),
                    labelClass: "labels",
                    labelStyle: { opacity: 0.65 }
                });
                mgr.addMarker(marker1, 1, 6);
            }
            else {
                var marker1 = new MarkerWithLabel({
                    position: latLng,
                    draggable: true,
                    raiseOnDrag: true,
                    icon: "../Content/libs/googleMap/m3.png",
                    map: map,
                    labelContent: cities[i].value.toString(),
                    labelAnchor: new google.maps.Point(8, 40),
                    labelClass: "labels",
                    labelStyle: { opacity: 0.65 }
                });
                mgr.addMarker(marker1, 7, 14);
            }
        }
    }
    function styleFeature(feature) {
        return {
            strokeColor: 'black',
            fillColor: 'rgba(78, 78, 78, 0.25)',
            fillOpacity: 0.75,
            visible: true,
            strokeWeight: 1

        };
    }
}