function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

document.getElementById('send-btn').addEventListener('click', async () => {
    const customerId = document.getElementById('customer-id-input').value;
    const day = document.getElementById('day-input').value;
    const month = document.getElementById('month-input').value;
    const year = document.getElementById('year-input').value;

    if (!customerId || !day || !month || !year) {
        alert('Please fill out all fields');
        return;
    }
    try {
        const authToken = getCookie("AuthToken");
        const response = await axios.post('/api/v2/search/GetBookingInformation', {
            customerId: parseInt(customerId),
            day: parseInt(day),
            month: parseInt(month),
            year: parseInt(year)
        }, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json'
            }
        });

        const bookings = response.data;

        const tableBody = document.getElementById('result-table').querySelector('tbody');
        tableBody.innerHTML = '';

        bookings.forEach(booking => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${booking.booking_id}</td>
                <td>${booking.booking_status_code}</td>
                <td>${booking.customer_id}</td>
                <td>${booking.reg_number}</td>
                <td>${booking.date_from}</td>
                <td>${booking.date_to}</td>
                <td>${booking.confirmation_letter_sent_yn}</td>
                <td>${booking.payment_received_yn}</td>
            `;
            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error fetching booking information:', error);
        alert('Failed to fetch booking information');
    }
});
