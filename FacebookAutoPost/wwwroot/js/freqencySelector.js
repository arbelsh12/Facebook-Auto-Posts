const select = document.querySelector('select');
const para = document.querySelector('p');

select.addEventListener('change', setFrequency);

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

function setFrequency() {
    const choice = select.value;

    if (choice === 'day') {
        para.textContent = 'day';
        dayFrequencySelected();
    }
    else if (choice === 'week') {
        para.textContent = 'week';
        weekFrequencySelected();
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
  