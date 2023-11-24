import axios from "axios";
const acounts = {
  namespaced: true,
  state: {
    certificate: null,
    data_post: {
      name: null,
      id: null,
    },
    err: {
      name: null,
    },
    alert: false,
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.certificate.filter((e) => e.id == id);
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.certificate = value;
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/certificate");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/certificate", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/certificate/" + state.data_post.id,
        state.data_post
      );
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/certificate/" + id);
      const data = await axios.get("http://localhost:3000/certificate");
      context.commit("set_value", data.data);
    },
  },
};
export default acounts;
