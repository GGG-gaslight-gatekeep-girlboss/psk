import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const EditProduct = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  // Simuliuojam produktą – vietoj to čia galėtų būti fetch iš API
  const mockProduct = {
    name: 'Espresso Macchiato',
    description: 'no stresso no stresso, no need to be depresso',
    price: '3.99',
    quantity: '30',
    imageName: 'coffee-img.png',
  };

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [quantity, setQuantity] = useState('');
  const [image, setImage] = useState<File | null>(null);

  useEffect(() => {
    // Užkraunam duomenis (čia gali būt fetch pagal id)
    setName(mockProduct.name);
    setDescription(mockProduct.description);
    setPrice(mockProduct.price);
    setQuantity(mockProduct.quantity);
  }, [id]);

  const handleSave = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Saving:', { name, description, price, quantity, image });
    navigate('/product-management');
  };

  const handleDelete = () => {
    console.log('Deleting product', id);
    navigate('/product-management');
  };

  const handleCancel = () => {
    navigate('/product-management');
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setImage(e.target.files[0]);
    }
  };

  return (
    <div style={{ position: 'relative', height: '100vh', width: '100vw', paddingTop: '5rem' }}>
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
          <a
            href="/"
            style={{
              fontSize: '1.5rem',
              fontWeight: 'bold',
              textDecoration: 'underline',
              color: 'black',
            }}
          >
            Coffee Shop
          </a>
        </div>

        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '0.5rem' }}>
          <span style={{ fontSize: '1.5rem' }}>🛒</span>
          <span style={{ fontSize: '1.5rem' }}>👤</span>
        </div>
      </header>

      {/* Forma */}
      <div style={{ display: 'flex', justifyContent: 'center', marginTop: '2rem' }}>
        <form onSubmit={handleSave} style={{ display: 'flex', flexDirection: 'column', width: '300px', gap: '1rem' }}>
          <h2 style={{ textAlign: 'center' }}>Edit product</h2>

          <label>
            Name
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Description
            <input
              type="text"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Price
            <input
              type="number"
              step="0.01"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Quantity
            <input
              type="number"
              value={quantity}
              onChange={(e) => setQuantity(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Image
            <input
              type="file"
              onChange={handleFileChange}
              style={{ width: '100%' }}
            />
            <div style={{ fontSize: '0.9rem', color: '#555', marginTop: '0.25rem' }}>
              {image ? image.name : mockProduct.imageName}
            </div>
          </label>

          {/* Mygtukai */}
          <div style={{ display: 'flex', justifyContent: 'space-between', marginTop: '1rem' }}>
            <button
              type="button"
              onClick={handleDelete}
              style={{
                backgroundColor: '#dc3545',
                color: 'white',
                border: 'none',
                padding: '0.5rem 1rem',
                borderRadius: '5px',
                cursor: 'pointer',
              }}
            >
              Delete
            </button>

            <button
              type="button"
              onClick={handleCancel}
              style={{
                backgroundColor: 'white',
                color: 'black',
                border: '1px solid black',
                padding: '0.5rem 1rem',
                borderRadius: '5px',
                cursor: 'pointer',
              }}
            >
              Cancel
            </button>

            <button
              type="submit"
              style={{
                backgroundColor: '#28a745',
                color: 'white',
                border: 'none',
                padding: '0.5rem 1rem',
                borderRadius: '5px',
                cursor: 'pointer',
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

export default EditProduct;
