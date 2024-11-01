document.getElementById('send-btn').addEventListener('click', async () => {
    const modelCode = document.getElementById('model-code-input').value;

    if (!modelCode) {
        alert('Please enter a model code');
        return;
    }

    try {
        const response = await axios.post('/api/search/ModelInformation', { model_code: modelCode });
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