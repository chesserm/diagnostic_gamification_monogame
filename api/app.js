const express = require('express');

const db = require('./db');
const AuthRoute = require('./routes/auth');

const app = express();
app.use(express.json());
app.use(express.urlencoded({extended: true}));

const PORT = process.env.PORT || 5000;

db.connect(() => {
  app.listen(PORT, () => {
    console.log(`Server started on port ${PORT}`);
  });
});

app.use('/api', AuthRoute);