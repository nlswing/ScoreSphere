const connection = new signalR.HubConnectionBuilder()
    .withUrl("/matchcenterhub")
    .configureLogging(signalR.LogLevel.Information)
    .build();


$(document).ready(function () {
    loadMatches();
    connection.start()
});

var allMatches = [];

connection.onclose(async () => {
    await start();
});

function loadMatches() {
    console.log("Loading matches...");
    $.get("/api/Matches", null, function (response) {
        console.log("Received matches from API:", response);

        allMatches = response;

        

        response.sort(function(a, b) {
            var dateA = new Date(a.date);
            var dateB = new Date(b.date);
            return dateA - dateB;
        });

        console.log("Sorted matches:", response);

        // Call bindMatches after sorting is complete
        bindMatches(response);
        console.log("Matches loaded successfully.");
        
    });
}

function bindMatches(matches) {
    var html = "";
    $("#listMatches").html("");
    $.each(matches,
        function(index, match) {
            html += "<tr data-match-id='" + match.id + "'>";

            
            html += "<td class='match-date'>";
            var matchDate = new Date(match.date);
            
            
            var formattedTime = matchDate.getHours().toString().padStart(2, '0') + ":" +
                                matchDate.getMinutes().toString().padStart(2, '0');
            
            html += matchDate.toLocaleDateString() + " " + formattedTime;
            html += "</td>";

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
    console.log("SignalR RefreshMatchCenter event received. Connection state:", connection.state);
    loadMatches();
});


function filterMatches(status) {
    
    var today = new Date();

    
    var filteredMatches = [];
    switch (status) {
        case 'all':
            filteredMatches = allMatches;
            break;
        case 'scheduled':
            filteredMatches = allMatches.filter(match => new Date(match.date) > today);
            break;
        case 'finished':
            filteredMatches = allMatches.filter(match => new Date(match.date) <= today);
            break;
        default:
            break;
    }

     bindMatches(filteredMatches);
}