const select = document.querySelector('select');
const para = document.querySelector('p');

select.addEventListener('change', setFrequency);

function setTime() {

    const selectDay = document.querySelector('.selectTimeDay');
    const choiceDay = selectDay.value;

    if (choiceDay === 'Specific') {

        // Get the element where the inputs will be added to
        var containerTime = document.getElementById("time-freqency-selector-group-container");

        if (containerTime === null) {
            var containerFather = document.getElementById("freqency-selector-group-container");
            const timeFreqDiv = document.createElement("div");

            timeFreqDiv.id = "time-freqency-selector-group-container";
            containerFather.appendChild(timeFreqDiv);

            containerTime = document.getElementById("time-freqency-selector-group-container");
        }
        // Remove every children it had before
        while (containerTime.hasChildNodes()) {
            containerTime.removeChild(containerTime.lastChild);
        }

        // Append a node with the text
        containerTime.appendChild(document.createTextNode("Please enter the requested time:"));
        // Create a <select> element, set its type and name attributes
        const timeLabel = document.createElement("label");
        timeLabel.for = "timeDay";
        timeLabel.className = "control-label";
        //timeLabel.text = "Please enter the requested time";

        const timeInput = document.createElement("input");
        timeInput.for = "timeDay";
        timeInput.className = "form-control";

        containerTime.appendChild(timeLabel);
        containerTime.appendChild(timeInput);
    }
    else {
        // Get the element where the inputs will be added to
        var container = document.getElementById("time-freqency-selector-group-container");
        // Remove every children it had before
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
    }
}

function setDayOfWeek() {
    //day-week-freqency-selector-group-container

    const selectDayWeek = document.querySelector('.selectDayWeek');
    const choiceDay = selectDayWeek.value;

    if (choiceDay === 'Specific') {

        // Get the element where the inputs will be added to
        var containerTime = document.getElementById("day-week-freqency-selector-group-container");

        if (containerTime === null) {
            var containerFather = document.getElementById("freqency-selector-group-container");
            const timeFreqDiv = document.createElement("div");

            timeFreqDiv.id = "day-week-freqency-selector-group-container";
            containerFather.appendChild(timeFreqDiv);

            containerTime = document.getElementById("day-week-freqency-selector-group-container");
        }
        // Remove every children it had before
        while (containerTime.hasChildNodes()) {
            containerTime.removeChild(containerTime.lastChild);
        }

        // Append a node with the text
        containerTime.appendChild(document.createTextNode("Please choose the requested day:"));
        // Create a <select> element, set its type and name attributes

        const selectDay = document.createElement("select");

        const optionSelect = document.createElement('option');
        optionSelect.text = "--Please choose an option--";
        optionSelect.value = "";

        const optionSunday = document.createElement('option');
        optionSunday.value = "Sunday";
        optionSunday.text = "Sunday";

        const optionMonday = document.createElement('option');
        optionMonday.value = "Monday";
        optionMonday.text = "Monday";

        const optionTuesday = document.createElement('option');
        optionTuesday.value = "Tuesday";
        optionTuesday.text = "Tuesday";

        const optionWednesday = document.createElement('option');
        optionWednesday.value = "Wednesday";
        optionWednesday.text = "Wednesday";

        const optionThursday = document.createElement('option');
        optionThursday.value = "Thursday";
        optionThursday.text = "Thursday";

        const optionFriday = document.createElement('option');
        optionFriday.value = "Friday";
        optionFriday.text = "Friday";

        const optionSaturday = document.createElement('option');
        optionSaturday.value = "Saturday";
        optionSaturday.text = "Saturday";

        selectDay.appendChild(optionSelect);

        selectDay.appendChild(optionSunday);
        selectDay.appendChild(optionMonday);
        selectDay.appendChild(optionTuesday);
        selectDay.appendChild(optionWednesday);
        selectDay.appendChild(optionThursday);
        selectDay.appendChild(optionFriday);
        selectDay.appendChild(optionSaturday);

        containerTime.appendChild(selectDay);
    }
    else {
        // Get the element where the inputs will be added to
        var container = document.getElementById("day-week-freqency-selector-group-container");
        // Remove every children it had before
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
    }
}

function setFrequency() {
    const choice = select.value;

    if (choice === 'day') {
        para.textContent = 'day';
        dayFrequencySelected();

        const selectSpecific = document.querySelector('.selectTimeDay');
        selectSpecific.addEventListener('change', setTime);
    }
    else if (choice === 'week') {
        para.textContent = 'week';
        weekFrequencySelected();

        const selectSpecific = document.querySelector('.selectDayWeek');
        selectSpecific.addEventListener('change', setDayOfWeek);
    
    }
    else if (choice === 'month') {
        para.textContent = 'month';
        monthFrequencySelected();

 
    }
    else {
        para.textContent = '';
        // Get the element where the inputs will be added to
        var container = document.getElementById("freqency-selector-group-container");
        // Remove every children it had before
        while (container.hasChildNodes()) {
            container.removeChild(container.lastChild);
        }
    }
}

function dayFrequencySelected() {
    // Get the element where the inputs will be added to
    const container = document.getElementById("freqency-selector-group-container");
    // Remove every children it had before
    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
    }
    // Append a node with the text
    container.appendChild(document.createTextNode("Please choose if you want a random time at the day or a specific time"));

    // Create a <select> element, set its type and name attributes
    const selectTimeDay = document.createElement("select");
    selectTimeDay.className = "selectTimeDay";

    const optionSelect = document.createElement('option');
    optionSelect.text = "--Please choose an option--";
    optionSelect.value = "";
    const optionRand = document.createElement('option');
    optionRand.value = "Rand";
    optionRand.text = "At a random time at the day";
    const optionSpecific = document.createElement('option');
    optionSpecific.value = "Specific";
    optionSpecific.text = "At a specific time I choose";

    selectTimeDay.appendChild(optionSelect);
    selectTimeDay.appendChild(optionRand);
    selectTimeDay.appendChild(optionSpecific);

    container.appendChild(selectTimeDay);
}

function weekFrequencySelected() {
    // Get the element where the inputs will be added to
    const container = document.getElementById("freqency-selector-group-container");
    // Remove every children it had before
    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
    }
    // Append a node with the text
    container.appendChild(document.createTextNode("Please choose if you want a random day at the week or a specific day"));
    // Create a <select> element, set its type and name attributes
    const selectDay = document.createElement("select");
    selectDay.className = "selectDayWeek"

    const optionSelect = document.createElement('option');
    optionSelect.text = "--Please choose an option--";
    optionSelect.value = "";
    const optionRand = document.createElement('option');
    optionRand.value = "Rand";
    optionRand.text = "At a random day at the week";
    const optionSpecific = document.createElement('option');
    optionSpecific.value = "Specific";
    optionSpecific.text = "At a specific day I choose";

    selectDay.appendChild(optionSelect);
    selectDay.appendChild(optionRand);
    selectDay.appendChild(optionSpecific);

    container.appendChild(selectDay);
}

function monthFrequencySelected() {
    // Get the element where the inputs will be added to
    const container = document.getElementById("freqency-selector-group-container");
    // Remove every children it had before
    while (container.hasChildNodes()) {
        container.removeChild(container.lastChild);
    }
    // Append a node with the text
    container.appendChild(document.createTextNode("Please choose if you want a random date at the month or a specific date"));
    // Create a <select> element, set its type and name attributes
    const selectDayMonth = document.createElement("select");
    const optionSelect = document.createElement('option');
    optionSelect.text = "--Please choose an option--";
    optionSelect.value = "";
    const optionRand = document.createElement('option');
    optionRand.value = "Rand";
    optionRand.text = "At a random date at the month";
    const optionSpecific = document.createElement('option');
    optionSpecific.value = "Specific";
    optionSpecific.text = "At a specific date I choose";

    selectDayMonth.appendChild(optionSelect);
    selectDayMonth.appendChild(optionRand);
    selectDayMonth.appendChild(optionSpecific);

    container.appendChild(selectDayMonth);
}


  