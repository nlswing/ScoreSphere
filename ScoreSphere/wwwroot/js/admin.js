$(document).ready(function () {
    loadMatches();
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
    var listMatches = $("#listMatches");

    listMatches.html("");
    $.each(matches, function(index, match) {
        // Row for each match
        html += "<tr data-match-id='" + match.id + "'>";

        // Team 1
        html += "<td>";
        html += "<img class='team-logo small-logo' src='/images/" + match.team1Logo + "'/>";
        html += "<span class='team-name'>" + match.team1Name + "</span>";
        html += "</td>";

        // Team 1 Goals
        html += "<td>";
        html += "<span data-team-id='" + match.team1Id + "' class='team-goals'>" + match.team1Goals + "</span>";
        html += "<input type='button' class='btn btn-success' value='Add Goal' data-match-id='" + match.id + "' data-team-id='" + match.team1Id + "' onclick='addGoal(this);' />";
        html += "</td>";

        // Separator
        html += "<td>";
        html += "<span class='team-separator'> &mdash; </span>";
        html += "</td>";

        // Team 2 Goals
        html += "<td>";
        html += "<span data-team-id='" + match.team2Id + "' class='team-goals'>" + match.team2Goals + "</span>";
        html += "<input type='button' class='btn btn-success' value='Add Goal' data-match-id='" + match.id + "' data-team-id='" + match.team2Id + "' onclick='addGoal(this);' />";
        html += "</td>";

        // Team 2
        html += "<td>";
        html += "<img class='team-logo small-logo' src='/images/" + match.team2Logo + "'/>";
        html += "<span class='team-name'>" + match.team2Name + "</span>";
        html += "</td>";

        // End of the row
        html += "</tr>";
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
    });
}
