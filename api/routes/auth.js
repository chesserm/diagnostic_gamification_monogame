const express = require('express');
const bcrypt = require('bcrypt');
const jwt = require('jsonwebtoken');

const db = require('../db');

const router = express.Router();


router.post('/register', async (req, res, next) => {
  const user = req.body.username.trim();
  const document = await db.get().findOne({ username: user });
  if (document) {
    return res.json({message: 'Username already exists'});
  }
  bcrypt.hash(req.body.password, 10, (err, hashedPass) => {
    if (err) {
      return res.json({error: err});
    }
    const userDoc = {
      username: user,
      password: hashedPass,
      experience: 0,
      numCoins: 0,
      experience_level: 'Student',
      stats: {
        total_correct: 0,
        total_attempted: 0,
        COPD: {
          correct: 0,
          attempted: 0,
          accuracy: 0,
          intervals: {
            '1-30': {
              attempted: 0,
              correct: 0,
              accuracy: 0
            }
          }
        },
        Pneuomnia: {
          correct: 0,
          attempted: 0,
          accuracy: 0,
          intervals: {
            '1-30': {
              attempted: 0,
              correct: 0,
              accuracy: 0
            }
          }
        },
        CHF: {
          correct: 0,
          attempted: 0,
          accuracy: 0,
          intervals: {
            '1-30': {
              attempted: 0,
              correct: 0,
              accuracy: 0
            }
          }
        }
      }
    }
    db.get().insertOne(userDoc)
      .then((result) => res.json({message: 'User Registered'}))
      .catch((err) => res.json({error: err}));
  });
});

router.post('/login', async (req, res, next) => {
  const username = req.body.username;
  const password = req.body.password;

  const user = await db.get().findOne({username});
  if (user) {
    bcrypt.compare(password, user.password, (err, result) => {
      if (err) {
        return res.json({error: err});
      }
      if (result) {
        let token = jwt.sign(
          {username: user.username},
          'eecs495IsGreat',
          {expiresIn: '24h'}
        );
        return res.json({message: 'Login Successful', token});
      } else {
        return res.json({message: 'Incorrect username or password'});
      }
    })
  } else {
    // Username is incorrect
    return res.json({message: 'Incorrect username or password'});
  }
});

module.exports = router;