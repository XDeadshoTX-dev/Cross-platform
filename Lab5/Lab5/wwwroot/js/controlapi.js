async function getBookingInfo(version) {
    const customerId = document.getElementById(`customer-id-input-${version}`).value;
    const day = document.getElementById(`day-input-${version}`).value;
    const month = document.getElementById(`month-input-${version}`).value;
    const year = document.getElementById(`year-input-${version}`).value;

    if (!customerId || !day || !month || !year) {
        alert("Please fill in all fields.");
        return;
    }

    const requestData = {
        customerId: parseInt(customerId),
        day: parseInt(day),
        month: parseInt(month),
        year: parseInt(year)
    };

    const apiUrl = `http://localhost:5178/api/${version}/search/GetBookingInformation`;

    try {
        const response = await axios.post(apiUrl, requestData, {
            headers: { 'Content-Type': 'application/json' }
        });

        document.getElementById(`response${version.toUpperCase()}`).innerText = JSON.stringify(response.data, null, 2);
    } catch (error) {
        console.error("Error fetching booking information:", error);
        document.getElementById(`response${version.toUpperCase()}`).innerText = "An error occurred. Please try again.";
    }
}
