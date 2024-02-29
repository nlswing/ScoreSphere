	
var connection = new signalR.HubConnectionBuilder().withUrl("/matchcenterhub").build();


$(document).ready(function () {
    loadMatches();
    connection.start();
});

function loadMatches() {
    $.get("/api/Matches", null, function (response) {
        // Sort matches by the name of the first team
        response.sort(function(a, b) {
            var nameA = a.team1Name.toUpperCase();
            var nameB = b.team1Name.toUpperCase();
            return nameA.localeCompare(nameB);
        });

        bindMatches(response);
    });
}

function bindMatches(matches) {
    var html = "";
    $("#listMatches").html("");
    $.each(matches,
        function(index, match) {
            html += "<tr data-match-id='" + match.id + "'>";

            html += "<td>";
            html += "<img class='team-logo small-logo' src='/images/" + match.team1Logo + "'/>";
            html += "<span class='team-name'>" + match.team1Name + "</span>";
            html += "</td>";

            html += "<td>";
            html += "<span data-team-id='" + match.team1Id + "' class='team-goals'>" + match.team1Goals + "</span>";
            html += "</td>";

            html += "<td>";
            html += "<span class='team-separator'> &mdash; </span>";
            html += "</td>";

            html += "<td>";
            html += "<span data-team-id='" + match.team2Id + "' class='team-goals'>" + match.team2Goals + "</span>";
            html += "</td>";

            html += "<td>";
            html += "<img class='team-logo small-logo' src='/images/" + match.team2Logo + "'/>";
            html += "<span class='team-name'>" + match.team2Name + "</span>";
            html += "</td>";

            html += "</tr>";
        });
    $("#listMatches").append(html);
}

connection.on("RefreshMatchCenter", function (match) {
    loadMatches();
});