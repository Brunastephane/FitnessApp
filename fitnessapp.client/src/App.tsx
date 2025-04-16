import { useEffect, useState } from 'react';
import './App.css';
import { Users } from './models/models';

function App() {
    const [users, setUsers] = useState<Users[]>([]);

    useEffect(() => {
        getAllUsersData();
    }, []);

    const contents = users.length === 0
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {users.map(user =>
                    <tr key={user.id}>
                        <td>{user.name}</td>
                        <td>{user.email}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Users</h1>
            {contents}
        </div>
    );

    async function getAllUsersData() {
        const response = await fetch('https://localhost:32768/api/Users');
        if (response.ok) {
            const data = await response.json();
            setUsers(data);
        } else {
            console.error("Error fetching data from API: ", response.statusText);
        }
    }
}

export default App;