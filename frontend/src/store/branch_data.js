import axios from "axios";
const acounts = {
  namespaced: true,
  state: {
    branch: null,
    data_post: {
      name: null,
      adress: null,
      id: null,
    },
    err: {
      name: null,
      adress: null,
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.branch.filter((e) => e.id == id);
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.branch = value;
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/branch");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/branch", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/branch/" + state.data_post.id,
        state.data_post
      );
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/branch/" + id);
      const data = await axios.get("http://localhost:3000/branch");
      context.commit("set_value", data.data);
    },
  },
};
export default acounts;
