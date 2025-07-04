import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Table from './components/Table';
import 'D:/Projetos/React/truco/src/styles/App.css'

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Table/>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
