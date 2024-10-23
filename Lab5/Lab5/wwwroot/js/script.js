const sendButton = document.getElementById('sendButton');
const outputConsole = document.getElementById('output-section');
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

        const p = outputConsole.appendChild(document.createElement("p"));
        p.innerHTML = response.data;
    } catch (error) {
        const p = outputConsole.appendChild(document.createElement("p"));
        p.innerHTML = `Error: ${error}`;
    }
});