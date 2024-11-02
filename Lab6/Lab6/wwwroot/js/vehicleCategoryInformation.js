function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

document.getElementById('send-btn').addEventListener('click', async () => {
    const categoryCode = document.getElementById('category-code-input').value;

    if (!categoryCode) {
        alert('Please enter a category code');
        return;
    }

    try {
        const authToken = getCookie("AuthToken");
        const response = await axios.post('/api/v1/search/VehicleCategoryInformation',
            {
                vehicle_category_code: categoryCode
            }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        });
        const description = response.data[0]?.vehicle_category_description || "No description found";
        document.getElementById('category-description').innerText = description;
    } catch (error) {
        console.error('Error fetching category information:', error);
        alert('Failed to fetch category information');
    }
});