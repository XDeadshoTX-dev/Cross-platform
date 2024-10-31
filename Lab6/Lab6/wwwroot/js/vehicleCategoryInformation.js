document.getElementById('send-btn').addEventListener('click', async () => {
    const categoryCode = document.getElementById('category-code-input').value;

    if (!categoryCode) {
        alert('Please enter a category code');
        return;
    }

    try {
        const response = await axios.post('/Search/VehicleCategoryInformation', { vehicle_category_code: categoryCode });
        const description = response.data[0]?.vehicle_category_description || "No description found";
        document.getElementById('category-description').innerText = description;
    } catch (error) {
        console.error('Error fetching category information:', error);
        alert('Failed to fetch category information');
    }
});