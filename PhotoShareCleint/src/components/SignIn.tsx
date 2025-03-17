import { useState } from "react";
import { TextField, Button, Typography, Box, Paper } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { loginUser } from '../store/userSlice';
import { RootState, AppDispatch } from '../store/store'; 

const SignIn = () => {
  const [userLogin, setUserLogin] = useState({ email: '', password: '' });
  const dispatch = useDispatch<AppDispatch>(); 
  const navigate = useNavigate();
  const msg = useSelector((state: RootState) => state.user.msg); // קבלת המסר מה-state

  const handleSubmit = async () => {
    const resultAction = await dispatch(loginUser(userLogin));
    if (loginUser.fulfilled.match(resultAction)) {
      navigate('/dashboard'); 
    }
  };

  return (
    <>
      <Box
        sx={{
          position: "fixed",
          top: 0,
          left: 0,
          height: "100vh",
          width: "100vw",
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          backgroundColor: "rgba(255, 255, 255, 0.5)",
          overflow: "hidden",
        }}
      >
        <Paper
          elevation={3}
          sx={{
            padding: 5,
            borderRadius: "12px",
            backgroundColor: "rgba(255, 255, 255, 0.8)",
            width: "400px",
            textAlign: "center",
            boxShadow: "0px 4px 10px rgba(0, 0, 0, 0.1)",
          }}
        >
          <Typography variant="h4" fontWeight="600" color="text.primary" gutterBottom>
            התחברות
          </Typography>
          <TextField
            label="UserEmail"
            value={userLogin.email}
            required
            onChange={({ target }) => setUserLogin({ ...userLogin, email: target.value })}
            fullWidth
            sx={{ marginBottom: 2 }}
          />
          <TextField
            label="UserPassword"
            type="password"
            required
            value={userLogin.password}
            onChange={({ target }) => setUserLogin({ ...userLogin, password: target.value })}
            fullWidth
            sx={{ marginBottom: 2 }}
          />
          <Button
            variant="contained"
            size="large"
            onClick={handleSubmit}
            sx={{ backgroundColor: "#000000", "&:hover": { backgroundColor: "#333333" } }}
          >
            כניסה
          </Button>
          {msg && <div style={{ color: 'red' }}>{msg}</div>} {/* הצגת השגיאה כאן */}
        </Paper>
      </Box>
    </>
  );
};

export default SignIn;
