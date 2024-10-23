const sendButton = document.getElementById('sendButton');
sendButton.addEventListener('click', async (event) => {
    event.preventDefault();

    const inputFile = document.getElementById('inputFile').files[0];
    const lab = document.getElementById('lab').value;

    const formData = new FormData();
    formData.append('inputFile', inputFile);
    formData.append('lab', lab);

    try {

        const response = await axios.post('/Home/StartLab', formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });

        console.log(response.data);
    } catch (error) {
        console.log('Error:', error);
    }
});