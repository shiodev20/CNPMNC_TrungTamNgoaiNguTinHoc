import axios from "axios";
const exam = {
  namespaced: true,
  state: {
    exam: null,
    data_post: {
      id: null,
      language_id: null,
      certificate_id: null,
      name: null,
      price: null,
      decription: null,
      exam_type_id: null,
    },
    err: {
      name: null,
      languages: null,
      certificate: null,
      price: null,
      exam_type: null,
    },
  },
  mutations: {
    set_value: (state, value) => {
      state.exam = value;
    },
  },
  getters: {
    getbyid: (state) => (id) => {
      return state.exam.filter((e) => e.id == id)[0];
    },
  },
  actions: {
    async get_all({ commit }) {
      const data = await axios.get("http://localhost:3000/exam");
      commit("set_value", data.data);
    },
    async create({ commit, state }) {
      console.log(commit);
      await axios.post("http://localhost:3000/exam", state.data_post);
    },
    async update({ commit, state }) {
      console.log(commit);
      await axios.put(
        "http://localhost:3000/exam/" + state.data_post.id,
        state.data_post
      );
      const data = await axios.get("http://localhost:3000/exam");
      commit("set_value", data.data);
    },
    async delete(context, id) {
      await axios.delete("http://localhost:3000/exam/" + id);
      const data = await axios.get("http://localhost:3000/exam");
      context.commit("set_value", data.data);
    },
  },
};
export default exam;
