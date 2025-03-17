import { useForm, Controller } from "react-hook-form";
import { TextField, Button, Typography, Box, Paper } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";
import { useState } from "react";

interface SignupForm {
  UserName: string;
  Password: string;
  Name: string;
  Phone: string;
  Email: string;
  Tz: string;
}

const Signup = () => {
  const { control, handleSubmit, formState: { errors, isValid } } = useForm<SignupForm>({ mode: "onChange" });
  const [msg, setMsg] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const navigate = useNavigate();

  const onSend = async (data: SignupForm) => {
    setLoading(true);
    setMsg("");

    try {
      const res = await axios.post(
        "http://localhost:8080/api/user/signup",
        data,
        { headers: { "Content-Type": "application/json" } }
      );

      if (res.data && res.data.Id) {
        setMsg("ההרשמה בוצעה בהצלחה! 🎉");
        navigate("/dashboard");
      } else {
        setMsg("שגיאה בהרשמה. נסה שוב.");
      }
    } catch (error: any) {
      if (error.response) {
        setMsg("😜לחץ כאן לכניסה אתה כבר רשום במאגר");
      } else {
        setMsg("שגיאה בחיבור לשרת. נסה שוב מאוחר יותר.");
      }
    } finally {
      setLoading(false);
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
            הרשמה
          </Typography>
          <form onSubmit={handleSubmit(onSend)}>
            <Controller
              name="UserName"
              control={control}
              defaultValue=""
              rules={{ required: "UserName is required" }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="UserName"
                  error={!!errors.UserName}
                  helperText={errors.UserName ? errors.UserName.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Controller
              name="Password"
              control={control}
              defaultValue=""
              rules={{
                required: "Password is required",
                minLength: {
                  value: 6,
                  message: "Password must be at least 6 characters long"
                }
              }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Password"
                  type="password"
                  error={!!errors.Password}
                  helperText={errors.Password ? errors.Password.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Controller
              name="Name"
              control={control}
              defaultValue=""
              rules={{ required: "Name is required" }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Name"
                  error={!!errors.Name}
                  helperText={errors.Name ? errors.Name.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Controller
              name="Phone"
              control={control}
              defaultValue=""
              rules={{ required: "Phone is required" }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Phone"
                  error={!!errors.Phone}
                  helperText={errors.Phone ? errors.Phone.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Controller
              name="Email"
              control={control}
              defaultValue=""
              rules={{
                required: "Email is required",
                pattern: {
                  value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/,
                  message: "Invalid email address"
                }
              }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Email"
                  error={!!errors.Email}
                  helperText={errors.Email ? errors.Email.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Controller
              name="Tz"
              control={control}
              defaultValue=""
              rules={{ required: "Tz is required" }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label="Tz"
                  error={!!errors.Tz}
                  helperText={errors.Tz ? errors.Tz.message : ''}
                  required
                  fullWidth
                  sx={{ marginBottom: 2 }}
                />
              )}
            />
            <Button
              type="submit"
              variant="contained"
              size="large"
              sx={{ backgroundColor: "#000000", "&:hover": { backgroundColor: "#333333" } }}
              disabled={!isValid || loading}
            >
              {loading ? "ביצוע הרשמה..." : "הרשמה"}
            </Button>
          </form>
          {msg === "😜לחץ כאן לכניסה אתה כבר רשום במאגר" && (
            <Link to="/login">
              <Typography variant="body2" align="center">
                להתחברות הקליקו כאן
              </Typography>
            </Link>
          )}
          {msg && (
            <Typography variant="body2" color="error" align="center">
              {msg}
            </Typography>
          )}
        </Paper>
      </Box>
    </>
  );
};

export default Signup;