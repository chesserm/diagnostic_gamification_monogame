const MongoClient = require('mongodb').MongoClient;
const mongoDbUrl = 'mongodb+srv://mahirtaq:eecs495pass@diagnosticcluster.reeyh.mongodb.net/db1?retryWrites=true&w=majority';
let client = new MongoClient(mongoDbUrl, { useNewUrlParser: true });
let collection;

function connect(callback){
    client.connect(err => {
      collection = client.db('db1').collection('users');
      callback();
    });
}
function get(){
    return collection;
}

function close(){
    client.close();
}

module.exports = {
    connect,
    get,
    close
};