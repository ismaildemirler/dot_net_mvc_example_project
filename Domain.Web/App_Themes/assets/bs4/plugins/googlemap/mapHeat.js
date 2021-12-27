
function GetHeatMap(_url, divID) {
    $.ajax({
        url: _url,
        data: {},
        cache: false,
        type: "POST",
        success: function (data) {
            initMap(data, divID);
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}

function initMap(citiesTmp, divID) {

    var cities = [];
    var data = [];
    var minCity = 0;
    var maxCity = 0;
    var minRegion = 0;
    var maxRegion = 0;
    var regions = [0, 0, 0, 0, 0, 0, 0];
    var map = null;

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

    calculateForCity();
    calculateForRegion();

    map = new google.maps.Map(document.getElementById(divID), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 5,
        center: { lat: 39.0311652, lng: 35.6385272 }
    });
    data = map.data.addGeoJson(regionsPolygon);
    map.data.addListener('click', function (e) {
        var zoom = map.getZoom();
        var infowindow = null;
        if (zoom < 6) {
            infowindow = new google.maps.InfoWindow({
                content: '<b>' + e.feature.f.title + '</b> , Değer : ' + regions[e.feature.f.description - 1]
            });
            infowindow.setPosition({ lat: regionsMarker[e.feature.f.description - 1].geometry.coordinates[0], lng: regionsMarker[e.feature.f.description - 1].geometry.coordinates[1] });
        }
        else {
            infowindow = new google.maps.InfoWindow({
                content: '<b>' + e.feature.f.title + '</b> , Değer : ' + cities[e.feature.f.description - 1].value
            });
            infowindow.setPosition({ lat: citiesMarker[e.feature.f.description - 1].geometry.coordinates[0], lng: citiesMarker[e.feature.f.description - 1].geometry.coordinates[1] });
        }
        infowindow.open(map);
    });
    google.maps.event.addListener(map, 'zoom_changed', function (event) {
        if (data != null || data != undefined) {
            for (var i = 0; i < data.length; i++) {
                map.data.remove(data[i]);
            }
        }

        var zoom = map.getZoom();
        if (zoom < 6) {
            //bölgesel olarak göster
            data = map.data.addGeoJson(regionsPolygon);
        }
        else {
            //il bazlı göster
            data = map.data.addGeoJson(citiesPolygon);
        }
    });

    map.data.setStyle(styleFeature);

    function styleFeature(feature) {
        var value = 0;
        var currentzoom = map.getZoom();
        var min = 0;
        var max = 0;

        if (currentzoom < 6) {
            //bölgesel olarak göster
            value = regions[parseInt(feature.getProperty('description')) - 1];
            min = minRegion;
            max = maxRegion;
        }
        else {
            //il bazlı göster
            value = parseInt(cities[parseInt(feature.getProperty('description')) - 1].value);
            min = minCity;
            max = maxCity;
        }

        var range = (max - min) / 7;
        var renk = "";

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

        return {
            strokeColor: '#fff',
            fillColor: renk,
            fillOpacity: 0.75,
            visible: true,
            strokeWeight: 0.5

        };
    }

    function calculateForCity() {
        minCity = parseInt(cities[0].value);
        maxCity = parseInt(cities[0].value);
        for (var i = 1; i < cities.length; i++) {
            if (minCity > parseInt(cities[i].value)) {
                minCity = parseInt(cities[i].value);
            }
            if (maxCity < parseInt(cities[i].value)) {
                maxCity = parseInt(cities[i].value);
            }
        }
    }
    function calculateForRegion() {
        minRegion = regions[0];
        maxRegion = regions[0];
        for (var i = 1; i < regions.length; i++) {
            if (minRegion > regions[i]) {
                minRegion = regions[i];
            }
            if (maxRegion < regions[i]) {
                maxRegion = regions[i];
            }
        }
    }

}

