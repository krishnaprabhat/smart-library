export const environment = {
  production: true,
  apiUrl: typeof window !== 'undefined' && (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1')
    ? 'http://localhost:5247/api'
    : 'https://smart-library-api.onrender.com/api' // Replace with your actual Render API URL if different
};
