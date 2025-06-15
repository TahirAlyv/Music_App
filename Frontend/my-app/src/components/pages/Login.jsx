import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from '../../api/axios';
import './Login.css';
const API = import.meta.env.VITE_API_BASE_URL;

function Login() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();  

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const res = await axios.post(`${API}/api/Auth/login`, {
        userName,
        password,
      });

      localStorage.setItem("token", res.data);
      alert('Login successful');
      navigate('/Home');
    } catch (err) {
      alert('Invalid login');
    }
  };

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleLogin}>
        <h2>Login</h2>
        <input
          type="text"
          placeholder="Username"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">Login</button>

        <p style={{ marginTop: '16px', fontSize: '14px',color:"white" }}>
          Don't have an account?{" "}
          <span
            style={{ color: '#00c896', cursor: 'pointer', textDecoration: 'underline' }}
            onClick={() => navigate('/register')}
          >
            Register
          </span>
        </p>
      </form>
    </div>
  );
}

export default Login;
