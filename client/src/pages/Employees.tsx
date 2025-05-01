import { Link, useNavigate } from 'react-router-dom';

const Employees = () => {
  const navigate = useNavigate();
//pavyzdiniai duomenys
  const employees = [
    { id: 1, firstName: 'John', lastName: 'Smith' },
    { id: 2, firstName: 'Mark', lastName: 'Kevin' },
  ];

  const handleLogout = () => {
    navigate('/login');
  };

  return (
    <div style={{ position: 'relative', minHeight: '100vh', width: '100vw' }}>
      {/* Viršutinė juosta tokia pati kaip Home */}
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

      {/* Turinys žemiau headerio */}
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'start',
          paddingTop: '8rem',
          paddingBottom: '2rem',
        }}
      >
        <div style={{ width: '100%', maxWidth: '800px' }}>
          <div
            style={{
              display: 'flex',
              justifyContent: 'space-between',
              alignItems: 'center',
              marginBottom: '1rem',
            }}
          >
            <h2>Employees</h2>
            <button onClick={() => navigate('/add-employee')}>Add employee</button>
          </div>

          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr style={{ backgroundColor: '#eee' }}>
                <th style={{ padding: '0.5rem', border: '1px solid #ccc' }}>First name</th>
                <th style={{ padding: '0.5rem', border: '1px solid #ccc' }}>Last name</th>
                <th style={{ padding: '0.5rem', border: '1px solid #ccc' }}></th>
              </tr>
            </thead>
            <tbody>
              {employees.map((emp, idx) => (
                <tr key={emp.id} style={{ backgroundColor: idx % 2 === 0 ? '#fff' : '#f5f5f5' }}>
                  <td style={{ padding: '0.5rem', border: '1px solid #ccc' }}>{emp.firstName}</td>
                  <td style={{ padding: '0.5rem', border: '1px solid #ccc' }}>{emp.lastName}</td>
                  <td style={{ padding: '0.5rem', border: '1px solid #ccc' }}>
                    <Link to={`/edit-employee/${emp.id}`}>Edit</Link>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default Employees;
