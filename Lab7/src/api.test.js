const axios= require('axios');

const API = 'http://localhost:5178/api/v1/search/'
test('Get booking information', async () => {
    const response = await axios.post( `${API}GetBookingInformation`, {
        customerId: 3,
        day: 10,
        month: 11,
        year: 2024
    }, {
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const bookings = response.data;
    const booking = bookings[0];

    expect(booking).toHaveProperty('booking_id');
    expect(booking).toHaveProperty('booking_status_code');
    expect(booking).toHaveProperty('customer_id', 3);
    expect(booking).toHaveProperty('reg_number');
});

test('should fetch vehicle category information', async () => {
    const response = await axios.post(`${API}VehicleCategoryInformation`, {
        vehicle_category_code: 'SEDAN'
    }, {
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const vehicleCategories = response.data;

    expect(Array.isArray(vehicleCategories)).toBe(true);
    expect(vehicleCategories.length).toBeGreaterThan(0);

    const category = vehicleCategories[0];
    expect(category).toHaveProperty('vehicle_category_description', 'Sedan');
});

test('should fetch model information', async () => {
    const requestData = {
        model_code: 'MOD123'
    };

    const response = await axios.post(`${API}ModelInformation`, requestData, {
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const models = response.data;

    expect(Array.isArray(models)).toBe(true);
    expect(models.length).toBeGreaterThan(0);

    const model = models[0];
    expect(model).toHaveProperty('model_name', 'Model X');
    expect(model).toHaveProperty('daily_hire_rate');
});