import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from '../../api/axios';
import './Register.css';

function Register() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');
  const [fullName, setFullName] = useState('');        
  const [profilImage, setProfilImage] = useState('');  

  const navigate = useNavigate();  

  const handleRegister = async (e) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:5000/api/Auth/register', {
        userName,
        password,
        email,
        fullName,
        profilImage,     
      });
      alert('Registration successful');
    } catch (err) {
      console.error(err);
      alert('Registration failed');
    }
  };

  return (
    <div className="register-container">
      <form className="register-form" onSubmit={handleRegister}>
        <h2>Register</h2>

        <input
          type="text"
          placeholder="Full Name"
          value={fullName}
          onChange={e => setFullName(e.target.value)}
        />

        <input
          type="text"
          placeholder="Username"
          value={userName}
          onChange={e => setUserName(e.target.value)}
        />

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />

      <input
  type="text"
  placeholder="Profile Image URL (optional)"
  value={profilImage}
  onChange={e => setProfilImage(e.target.value)}
  style={{ display: 'none' }}
/>

        <button type="submit">Register</button>

        <p style={{ marginTop: '16px', fontSize: '14px',color:"white" }}>
          Already have an account?{" "}
          <span
            style={{ color: '#00c896', cursor: 'pointer', textDecoration: 'underline' }}
            onClick={() => navigate('/login')}
          >
            Login
          </span>
        </p>
      </form>
    </div>
  );
}

export default Register;
