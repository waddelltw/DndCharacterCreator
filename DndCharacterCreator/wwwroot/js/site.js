"use strict"
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/*
 * This function is called when the user clicks the roll all stats button on the create and edit page for a character in the form.
 * It calls the rollSingleStat button for every stat to assign the value to each of them manually.
 */
function rollAllStats() {
    //just calls the roll single stat function for each of the stats manually
    rollSingleStat("strength");
    rollSingleStat("dexterity");
    rollSingleStat("constitution");
    rollSingleStat("intelligence");
    rollSingleStat("wisdom");
    rollSingleStat("charisma");
}

/*
 * This function is called when the user clicks the roll button on the create and edit page for a character in the form.
 * When this is called this function will 'roll' 4 d6 to simulate a stat roll for creating a character. It will drop the lowest value
 * and use the other three to use for that stat.
 */
function rollSingleStat(stat) {
    //rolls each d6 dice here
    let roll1 = Math.floor(Math.random() * 6) + 1;
    let roll2 = Math.floor(Math.random() * 6) + 1;
    let roll3 = Math.floor(Math.random() * 6) + 1;
    let roll4 = Math.floor(Math.random() * 6) + 1;

    const rollSet = [roll1, roll2, roll3, roll4];

    //sorts the array to have the lowest value at the end of the array
    rollSet.sort(function (a, b) { return b - a });

    //adds the total using only the highest three numbers
    let total = rollSet[0] + rollSet[1] + rollSet[2];

    //sets the value in the form input tag
    $(`#${stat}`).val(total);
}