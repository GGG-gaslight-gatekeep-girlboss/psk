import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { Link } from 'react-router-dom';

const EditEmployee = () => {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState('John');
  const [lastName, setLastName] = useState('Smith');
  const [email, setEmail] = useState('john.smith@email.com');
  const [password, setPassword] = useState('************');
  const [status, setStatus] = useState('Active');

  const handleSave = () => {
    // čia būtų API kvietimas redaguoti darbuotoją
    navigate('/employees');
  };

  const handleCancel = () => {
    navigate('/employees');
  };

  const handleDeactivate = () => {
    setStatus('Inactive');
  };

  const handleLogout = () => {
    navigate('/login');
  };

  return (
    <div style={{ minHeight: '100vh', width: '100vw', paddingTop: '4rem' }}>
      {/* Viršutinė juosta */}
      <header
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'flex-start',
          padding: '1rem',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
        }}
      >
        <div style={{ width: '100px' }}></div>

        <div style={{ textAlign: 'center', flexGrow: 1 }}>
          <Link to="/" style={{ fontSize: '1.5rem', fontWeight: 'bold', textDecoration: 'underline', color: 'black' }}>
            Coffee Shop
          </Link>
        </div>

        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '0.5rem' }}>
          <span style={{ fontSize: '1.5rem' }}>🛒</span>
          <span style={{ fontSize: '1.5rem' }}>👤</span>
          <button onClick={handleLogout}>Log out</button>
        </div>
      </header>

      {/* Forma per vidurį */}
      <div style={{ display: 'flex', justifyContent: 'center', marginTop: '4rem' }}>
        <form style={{ width: '400px', display: 'flex', flexDirection: 'column', gap: '1rem' }}>
          <h2>Edit employee</h2>

          <label>
            First name
            <input
              type="text"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              style={{ width: '100%' }}
            />
          </label>

          <label>
            Last name
            <input
              type="text"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              style={{ width: '100%' }}
            />
          </label>

          <label>
            Email
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              style={{ width: '100%' }}
            />
          </label>

          <label>
            Password
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              style={{ width: '100%' }}
            />
          </label>

          <label>
            Status
            <input
              type="text"
              value={status}
              disabled
              style={{ width: '100%', backgroundColor: '#eee' }}
            />
          </label>

          {/* Mygtukai */}
          <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: '1rem' }}>
            <button
              type="button"
              onClick={handleDeactivate}
              style={{
                backgroundColor: 'red',
                color: 'white',
                padding: '0.5rem 1rem',
                border: 'none',
                borderRadius: '4px',
              }}
            >
              Deactivate
            </button>

            <button
              type="button"
              onClick={handleCancel}
              style={{
                padding: '0.5rem 1rem',
                border: '1px solid #ccc',
                borderRadius: '4px',
              }}
            >
              Cancel
            </button>

            <button
              type="button"
              onClick={handleSave}
              style={{
                backgroundColor: 'green',
                color: 'white',
                padding: '0.5rem 1rem',
                border: 'none',
                borderRadius: '4px',
              }}
            >
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default EditEmployee;
