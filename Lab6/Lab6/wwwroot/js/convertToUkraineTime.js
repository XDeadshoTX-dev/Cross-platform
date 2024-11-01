document.getElementById('convert-btn').addEventListener('click', async () => {
    const datetimeInput = document.getElementById('datetime-input').value;
    const resultElement = document.getElementById('result');
    resultElement.textContent = "Converting...";

    try {
        const response = await axios.post('/api/search/ConvertToUkraineTime', {
            inputDate: datetimeInput
        }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = response.data;
        resultElement.textContent = `Converted Date and Time: ${data.convertedDate}`;
    } catch (error) {
        resultElement.textContent = `Error: ${error.response ? error.response.data : error.message}`;
    }
});
