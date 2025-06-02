Adicionar no Settings/Config.json

// PARA USO DO SENDER SMS INFOBIP 
  "infobip": { 
    "username": "",//login 
    "password": "",// senha
    
  }
  
// PARA USO DO SENDER SMS IAGENT
  "iagent": { 
    "username": "",//login 
    "password": "",// senha    
  }

//CONFIGURAÇÃO DE DISPARO DE E-MAIL

  "SERVICES": {
    "SMTP": {
      "TEMPLATE": "Template", //NOME DA PASTA ONDE SE ENCONTRA O TEMPLATE EM HTML
      "HOST": "smtp.gmail.com",
      "PORT": 587,
      "SSL": false,
      "EMAIL": "", //EMAIL SENDER
      "LOGIN": "", // LOGIN CASO USO DO SERVIÇO DA AMAZOM DE DISPARO DE EMAILS
      "NAME": "", // NOME DO APP
      "PASSWORD": "" // SENHA DE ACESSO AO EMAIL SENDER
    }
  }

//CONFIGURAÇÃO DE DISPARO PUSH ONESIGNAL

  "SERVICES": {
    "ONESIGNAL": [
      {
        "KEY": "", // BASIG AUTH DO PROJETO
        "SOUND": "default",
        "ICON": "default",
        "APPID": "" //APP ID DO PROJETO
      }]
}  
  
//CONFIGURAÇÃO PARA USO DO FIREBASE
  "FirebaseSettings": {
      "urlDataBase": "", //URL DO BANCO DE DADOS DO FIREBASE
      "apiKey": "", //CHAVE DA CONTA DO FIREBASE
      "socialKey": "", //CHAVE DA CONTA DA REDE SOCIAL
      "email": "", //EMAIL DE ACESSO
      "password": "", //SENHA DE ACESSO
      "useAuth": "", //INFORMA SE DESEJA UTILIZAR AUTENTICAÇÃO
      "typeAuth": 0, //TIPO DE AUTENTICAÇÃO
      "firebaseAuthType": 0, //TIPO DE AUTENTICAÇÃO SOCIAL DO FIREBASE
    }
  }

//CONFIGURAÇÃO PARA USO DO FIRESTORE

  "FireStoreSettings": {
  "fileCredentials": "",//local do json service account
  "projectId": ""
}, 


// typeAuth
  public enum TypeAuthFirebase
{
    None,
    Social,
    EmailPassword
}  
	
// firebaseAuthType
  public enum TypeAuthFirebase
{
	Facebook,
	Google,
	Github,
	Twitter,
	EmailAndPassword
}


