const connection = new signalR.HubConnectionBuilder()
    .withUrl("/matchcenterhub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

// Start the connection.
start();

$(document).ready(function () {
    loadMatches();
});

function loadMatches() {
    $.get("/api/Matches", null, function (response) {
        // Sort matches by ascending date.
        var liveMatches = response.filter(match => match.isLive);

        response.sort(function(a, b) {
            var dateA = new Date(a.date);
            var dateB = new Date(b.date);
            return dateA - dateB;
        });

        bindMatches(liveMatches);
    });
}

function bindMatches(matches) {
    var html = "";
    var listMatches = $("#listMatches");

    listMatches.html("");
    $.each(matches, function(index, match) {
        // Row for each match
        html += "<tr data-match-id='" + match.id + "'>";

html += "<td class='match-date'>";
var matchDate = new Date(match.date);

var formattedTime =
  matchDate.getHours().toString().padStart(2, '0') +
  ':' +
  matchDate.getMinutes().toString().padStart(2, '0');

html += matchDate.toLocaleDateString() + ' ' + formattedTime;
html += '</td>';

// Team 1
html += "<td>";
html +=
  "<img class='team-logo small-logo' src='/images/" +
  match.team1Logo +
  "'/>";
html += "<span class='team-name'>" + match.team1Name + '</span>';
html += '</td>';

// Team 1 Goals
html += "<td>";
html +=
  "<span data-team-id='" +
  match.team1Id +
  "' class='team-goals' style='padding: 5px;'>" +
  match.team1Goals +
  '</span>';
html +=
  "<input type='button' class='btn btn-success' value='Add Goal' data-match-id='" +
  match.id +
  "' data-team-id='" +
  match.team1Id +
  "' onclick='addGoal(this);' style='padding: 5px; font-size: 12px;' />";
html += '</td>';

// Separator
html += "<td>";
html += "<span class='team-separator'> &mdash; </span>";
html += '</td>';

// Team 2 Goals
html += "<td>";
html +=
  "<span data-team-id='" +
  match.team2Id +
  "' class='team-goals' style='padding: 5px;'>" +
  match.team2Goals +
  '</span>';
html +=
  "<input type='button' class='btn btn-success' value='Add Goal' data-match-id='" +
  match.id +
  "' data-team-id='" +
  match.team2Id +
  "' onclick='addGoal(this);' style='padding: 5px; font-size: 12px;' />";
html += '</td>';

// Team 2
html += "<td>";
html +=
  "<img class='team-logo small-logo' src='/images/" +
  match.team2Logo +
  "'/>";
html += "<span class='team-name'>" + match.team2Name + '</span>';
html += '</td>';

// End of the row
html += '</tr>';
    });

    // Append the HTML to the table
    listMatches.append(html);
}

function addGoal(element) {
    var data = {
        matchId: $(element).attr("data-match-id"),
        teamId: $(element).attr("data-team-id")
    };

    $.ajax({
        type: 'PUT',
        url: 'api/Matches',
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).done(function() {
        loadMatches();
    
        connection.invoke("SendMatchCenterUpdate").catch(function (err) {
            return console.error(err.toString());
        });
    
    });

    
}
