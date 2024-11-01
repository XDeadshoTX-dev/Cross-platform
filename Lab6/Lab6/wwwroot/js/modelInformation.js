function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

document.getElementById('send-btn').addEventListener('click', async () => {
    const modelCode = document.getElementById('model-code-input').value;

    if (!modelCode) {
        alert('Please enter a model code');
        return;
    }

    try {
        const authToken = getCookie("AuthToken");
        const response = await axios.post('/api/search/ModelInformation',
            {
                model_code: modelCode
            }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        });
        const modelDetails = response.data;

        const tableBody = document.getElementById('model-details');
        tableBody.innerHTML = ''; 

        modelDetails.forEach(model => {
            const row = document.createElement('tr');
            row.innerHTML = `
                        <td>${model.model_name}</td>
                        <td>${model.daily_hire_rate}</td>
                    `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error fetching model information:', error);
        alert('Failed to fetch model information');
    }
});