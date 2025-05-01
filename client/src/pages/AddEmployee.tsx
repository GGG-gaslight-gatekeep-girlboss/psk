import { Link } from 'react-router-dom';
import { useState } from 'react';

const AddEmployee = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // API logika pridėti darbuotoją čia
    console.log({ firstName, lastName, email, password });
  };

  return (
    <div style={{ position: 'relative', width: '100vw', height: '100vh' }}>
      {/* Viršus */}
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
          <Link
            to="/"
            style={{
              fontSize: '1.5rem',
              fontWeight: 'bold',
              textDecoration: 'underline',
              color: 'black',
            }}
          >
            Coffee Shop
          </Link>
        </div>

        <div style={{ display: 'flex', gap: '1rem', fontSize: '1.5rem' }}>
          <span>🛒</span>
          <span>👤</span>
        </div>
      </header>

      <div
        style={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            minHeight: '100vh',
        }}
        >

        <form
          onSubmit={handleSubmit}
          style={{
            display: 'flex',
            flexDirection: 'column',
            gap: '1rem',
            minWidth: '300px',
          }}
        >
          <h2 style={{ textAlign: 'center' }}>Add employee</h2>

          <label>First name</label>
          <input
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />

          <label>Last name</label>
          <input
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />

          <label>Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <label>Password</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <button
            type="submit"
            style={{
              marginTop: '1rem',
              backgroundColor: '#4da6ff',
              color: 'white',
              padding: '0.5rem',
              border: '1px solid gray',
              borderRadius: '5px',
            }}
          >
            Add employee
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddEmployee;
