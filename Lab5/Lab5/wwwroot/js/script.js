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

        const data = response.data;
        const buildingText = data.match(/Building[\s\S]*?(?=Testing)/)?.[0];
        const testingText = data.match(/Testing[\s\S]*?(?=Starting)/)?.[0];
        const runningText = data.match(/(Starting)[\s\S]*/)?.[0];

        const p1 = outputConsole.appendChild(document.createElement("p"));
        p1.innerHTML = buildingText.replace(/\n/g, '<br>');
        const p2 = outputConsole.appendChild(document.createElement("p"));
        p2.innerHTML = testingText.replace(/\n/g, '<br>');
        const p3 = outputConsole.appendChild(document.createElement("p"));
        p3.innerHTML = runningText.replace(/\n/g, '<br>');

    } catch (error) {
        const p = outputConsole.appendChild(document.createElement("p"));
        p.innerHTML = `Error: ${error}`;
    }
});