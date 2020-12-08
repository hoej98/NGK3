﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/WeatherForecastHub").build();

connection.start().catch(function (e) {
});

connection.on("NewMeasurements", function (time, name, lat, lon, temp, humid, pressure) {
    var m = "Time :" + time + ", " +
        "Name: " + name + ", " +
        "Lat: " + lat + ", " +
        "Lon: " + lon + ", " +
        "Temperature: " + temp + ", " +
        "Humidity: " + humid + ", " +
        "Pressure: " + pressure;
    var li = document.createElement("li");
    li.textContent = m;
    document.getElementById("measurementList").appendChild(li);

});