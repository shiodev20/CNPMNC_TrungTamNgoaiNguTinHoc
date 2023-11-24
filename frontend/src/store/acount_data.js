import axios from "axios";
const acounts = {
  namespaced: true,
  state: {
    emloyee: null,
    authencation: null,
    data_post: {
      emailUser: null,
      passwordUser: null,
    },
    err: {
      emailUser: null,
      passwordUser: null,
    },
  },
  mutations: {
    save_token: (state, value) => {
      state.emloyee = value;
    },
  },
  getters: {
    check_token: (state) => () => {
      if (state.emloyee) {
        state.authencation = state.emloyee.filter(
          (e) => e.token == localStorage.getItem("token")
        )[0];
        return state.emloyee.filter(
          (e) => e.token == localStorage.getItem("token")
        )[0];
      }
      return [];
    },
    getbyid: (state) => (id) => {
      return state.emloyee.filter((e) => e.id == id);
    },
    getbyemail: (state) => (emails) => {
      console.log(emails);
      return state.emloyee.filter((e) => e.email == emails);
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/user");
      commit("save_token", data.data);
    },
    async register({ commit }, datas) {
      console.log("Monh khoi");
      console.log(commit);
      await axios.post("http://localhost:3000/user", datas).then((e) => {
        console.log(e);
      });
    },
  },
};
export default acounts;
