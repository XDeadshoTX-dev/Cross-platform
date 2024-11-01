function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

document.getElementById('convert-btn').addEventListener('click', async () => {
    const datetimeInput = document.getElementById('datetime-input').value;
    const resultElement = document.getElementById('result');
    resultElement.textContent = "Converting...";

    const authToken = getCookie("AuthToken");
    try {
        const response = await axios.post('/api/search/ConvertToUkraineTime', {
            inputDate: datetimeInput
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        });
        const data = response.data;
        resultElement.textContent = `Converted Date and Time: ${data.convertedDate}`;
    } catch (error) {
        resultElement.textContent = `Error: ${error.response ? error.response.data : error.message}`;
    }
});
